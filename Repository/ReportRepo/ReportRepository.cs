using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Standard.DTOs.ReportDtos;
using Standard.Entities;
using ExcelDataReader;

namespace Repository.ReportRepo
{
    public class ReportRepository:IReportRepository
    {
        private readonly StockContext _context;

        public ReportRepository(StockContext context)
        {
            _context = context;
        }
        public async Task<string> ProcessExcelFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is empty");
            }
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\Uploads";
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    bool isHeaderSkipped = false;
                    do
                    {
                        while (reader.Read())
                        {
                            if (!isHeaderSkipped)
                            {
                                isHeaderSkipped = true;
                                continue;
                            }

                            var itemCode = reader.GetValue(0)?.ToString();
                            var item = await _context.Items.FirstOrDefaultAsync(i => i.ItemCode == itemCode);

                            if (item == null)
                            {
                                item = new Item
                                {
                                    ItemCode = itemCode,
                                    ItemNameEn = reader.GetValue(1)?.ToString(),
                                    ItemNameAr = reader.GetValue(2)?.ToString(),
                                    CatFk = Convert.ToInt32(reader.GetValue(3)),
                                    UniteFk = Convert.ToInt32(reader.GetValue(4)),
                                    //ItemExperationdate = Convert.ToDateTime(reader.GetValue(5)),
                                    ItemCreatedat = DateTime.Now,
                                    ItemUpdatedat = DateTime.Now,
                                    Delet = false
                                };
                                _context.Items.Add(item);
                                await _context.SaveChangesAsync();

                                var quantity = new Quantity
                                {
                                    ItemFk = item.ItemId,
                                    CurrentQuantity = Convert.ToInt32(reader.GetValue(5)),
                                    QuantityCreatedat = DateTime.Now,
                                    QuantityUpdatedat = DateTime.Now
                                };
                                _context.Quantities.Add(quantity);
                            }
                            else
                            {
                                var existingQuantity = await _context.Quantities.FirstOrDefaultAsync(q => q.ItemFk== item.ItemId);
                                if (existingQuantity != null)
                                {
                                    existingQuantity.CurrentQuantity += Convert.ToInt32(reader.GetValue(5));
                                    existingQuantity.QuantityUpdatedat = DateTime.Now;
                                }
                                else
                                {
                                    var newQuantity = new Quantity
                                    {
                                        ItemFk = item.ItemId,
                                        CurrentQuantity = Convert.ToInt32(reader.GetValue(5)),
                                        QuantityCreatedat = DateTime.Now,
                                        QuantityUpdatedat = DateTime.Now
                                    };
                                    _context.Quantities.Add(newQuantity);
                                }
                            }
                            await _context.SaveChangesAsync();
                        }
                    } while (reader.NextResult());
                }
            }
            return "Data inserted successfully!";
        }

        public async Task<List<ItemsQuantities>> getAllItemsQuantitiesInAllSubs()
        {
            var itemQuantities = await _context.Quantities
                .Include(q => q.ItemFkNavigation)
                .Select(qi => new ItemsQuantities
                {
                    ItemName = qi.ItemFkNavigation.ItemNameEn,
                    CurrentQuantity = qi.CurrentQuantity
                })
                .ToListAsync();
            return itemQuantities;
        }

        public async Task<List<ItemsQuantities>> getAllItemsQuantitiesBySubId(int subId)
        {
            var itemQuantities = await _context.SubItems
                .Include(q => q.ItemFkNavigation)
                .Where(s=>s.SubFk ==  subId)    
                .Select(qi => new ItemsQuantities
                {
                    ItemName = qi.ItemFkNavigation.ItemNameEn,
                    CurrentQuantity = qi.Quantity
                })
                .ToListAsync();
            return itemQuantities;
        }
    }
}
