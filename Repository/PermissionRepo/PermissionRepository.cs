﻿
using Microsoft.EntityFrameworkCore;
using Repository.Pagination;
using Standard.DTOs.PermissionDto;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PermissionRepo
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly StockContext _context;
        public PermissionRepository(StockContext context)
        {
            _context = context;
        }
        public async Task<List<DisplayPermissionsDto>> GetAllPermissions()
        {
            var permissions =await _context.Permissions
                .Include(p=>p.PermTypeFkNavigation)
                .Include(p=>p.SubFkNavigation)
                .Include(p=>p.DestinationSubFkNavigation)
                .Select(permission => new DisplayPermissionsDto
                {
                    PermId = permission.PermId,
                    PermCode = permission.PermCode,
                    PerTypeValueEn = permission.PermTypeFkNavigation.PerTypeValueEn,
                    PerTypeValueAr = permission.PermTypeFkNavigation.PerTypeValueAr,
                    SubNameEn = permission.SubFkNavigation.SubNameEn,
                    SubNameAr = permission.SubFkNavigation.SubNameAr,
                    DestinationSubNameEn = permission.DestinationSubFkNavigation.SubNameEn,
                    DestinationSubNameAr = permission.DestinationSubFkNavigation.SubNameAr,
                    PermCreatedat = permission.PermCreatedat,
                    ItemCount = permission.ItemPermissions.Count(),
                })
                .ToListAsync();
            return permissions;
        }
        public async Task<PaginatedResult<DisplayPermissionsDto>> GetAllPermissionsWithPagination(int pageNumber, int pageSize)
        {
            var permissionsQuery = _context.Permissions
                .Include(p => p.PermTypeFkNavigation)
                .Include(p => p.SubFkNavigation)
                .Include(p => p.DestinationSubFkNavigation)
                .Select(permission => new DisplayPermissionsDto
                {
                    PermId = permission.PermId,
                    PermCode = permission.PermCode,
                    PerTypeValueEn = permission.PermTypeFkNavigation.PerTypeValueEn,
                    PerTypeValueAr = permission.PermTypeFkNavigation.PerTypeValueAr,
                    SubNameEn = permission.SubFkNavigation.SubNameEn,
                    SubNameAr = permission.SubFkNavigation.SubNameAr,
                    DestinationSubNameEn = permission.DestinationSubFkNavigation.SubNameEn,
                    DestinationSubNameAr = permission.DestinationSubFkNavigation.SubNameAr,
                    PermCreatedat = permission.PermCreatedat,
                    ItemCount = permission.ItemPermissions.Count(),
                });

            return await permissionsQuery.PaginateAsync(pageNumber, pageSize);
        }
        public async Task<List<DisplayPermissionsDto>> GetPermissionsByDate(DateTime date)
        {
            bool existsDate = await _context.Permissions
                            .AnyAsync(p => p.PermCreatedat.HasValue && p.PermCreatedat.Value.Date == date.Date);

            if (!existsDate)
            {
                return new List<DisplayPermissionsDto>();
            }
            var permissions = await _context.Permissions
                                .Include(p => p.PermTypeFkNavigation)
                                .Include(p => p.SubFkNavigation)
                                .Include(p => p.DestinationSubFkNavigation)
                .Where(p => p.PermCreatedat.HasValue && p.PermCreatedat.Value.Date == date.Date)
                .Select(permission => new DisplayPermissionsDto
                {
                    PermId = permission.PermId,
                    PermCode = permission.PermCode,
                    PerTypeValueEn = permission.PermTypeFkNavigation.PerTypeValueEn,
                    PerTypeValueAr = permission.PermTypeFkNavigation.PerTypeValueAr,
                    SubNameEn = permission.SubFkNavigation.SubNameEn,
                    SubNameAr = permission.SubFkNavigation.SubNameAr,
                    DestinationSubNameEn = permission.DestinationSubFkNavigation.SubNameEn,
                    DestinationSubNameAr = permission.DestinationSubFkNavigation.SubNameAr,
                    PermCreatedat = permission.PermCreatedat,
                    ItemCount = permission.ItemPermissions.Count(),
                })
                .ToListAsync();
            return permissions;
        }
        public async Task<List<DisplayPermissionsDto>> GetPermissionByTypeId(int typeId)
        {
            bool existsType = await _context.Permissions.AnyAsync(p=>p.PermTypeFk == typeId);
            if (!existsType)
            {
                return new List<DisplayPermissionsDto>();
            }
            var permissions = await _context.Permissions
                                .Include(p => p.PermTypeFkNavigation)
                                .Include(p => p.SubFkNavigation)
                                .Include(p => p.DestinationSubFkNavigation)
                .Where(p => p.PermTypeFk == typeId)
                .Select(permission => new DisplayPermissionsDto
                {
                    PermId = permission.PermId,
                    PermCode = permission.PermCode,
                    PerTypeValueEn = permission.PermTypeFkNavigation.PerTypeValueEn,
                    PerTypeValueAr = permission.PermTypeFkNavigation.PerTypeValueAr,
                    SubNameEn = permission.SubFkNavigation.SubNameEn,
                    SubNameAr = permission.SubFkNavigation.SubNameAr,
                    DestinationSubNameEn = permission.DestinationSubFkNavigation.SubNameEn,
                    DestinationSubNameAr = permission.DestinationSubFkNavigation.SubNameAr,
                    PermCreatedat = permission.PermCreatedat,
                    ItemCount = permission.ItemPermissions.Count(),
                })
                .ToListAsync();
            return permissions;
        }
        public async Task<DisplayPermissionsDto> GetPermissionById(int id)
        {
            var permission = await _context.Permissions
                .Include(p => p.PermTypeFkNavigation)
                .Include(p => p.SubFkNavigation)
                .Include(p => p.DestinationSubFkNavigation)
                .Include(p=>p.ItemPermissions)
                .Where(p => p.PermId == id)
                .Select(permission => new DisplayPermissionsDto
                {
                    PermId = permission.PermId,
                    PermCode = permission.PermCode,
                    PerTypeValueEn = permission.PermTypeFkNavigation.PerTypeValueEn,
                    PerTypeValueAr = permission.PermTypeFkNavigation.PerTypeValueAr,
                    SubNameEn = permission.SubFkNavigation.SubNameEn,
                    SubNameAr = permission.SubFkNavigation.SubNameAr,
                    DestinationSubNameEn = permission.DestinationSubFkNavigation.SubNameEn,
                    DestinationSubNameAr = permission.DestinationSubFkNavigation.SubNameAr,
                    PermCreatedat = permission.PermCreatedat,
                    ItemCount = permission.ItemPermissions.Count(),
                    Items = permission.ItemPermissions
                    .Select(permItem=> new PermissionItemDto 
                    { 
                        ItemId= permItem.ItemFk,
                        ItemCode = permItem.ItemFkNavigation.ItemCode,
                        ItemNameEn = permItem.ItemFkNavigation.ItemNameEn,
                        ItemNameAr = permItem.ItemFkNavigation.ItemNameAr,
                        CatNameEn = permItem.ItemFkNavigation.CatFkNavigation.CatNameEn,
                        CatNameAr = permItem.ItemFkNavigation.CatFkNavigation.CatNameAr,
                        UnitNameEn = permItem.ItemFkNavigation.UniteFkNavigation.UnitNameEn,
                        UnitNameAr = permItem.ItemFkNavigation.UniteFkNavigation.UnitNameAr,
                        Quantity = permItem.Quantity
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            return permission;
        }
        public async Task<string> GenerateNextPermissionCode()
        {
            var activeFormat = await _context.CodeFormats
                .FirstOrDefaultAsync(cf => cf.IsActive);

            if (activeFormat == null)
            {
                return null; 
            }

            string prefix = activeFormat.Format;

            var existingCodes = await _context.Permissions
                .Where(p => p.PermCode.StartsWith(prefix))
                .Select(p => p.PermCode)
                .ToListAsync();

            int highestNumber = 0;
            foreach (var code in existingCodes)
            {
                var numberPart = code.Substring(prefix.Length);
                if (int.TryParse(numberPart, out int number))
                {
                    highestNumber = Math.Max(highestNumber, number);
                }
            }

            int newNumber = highestNumber + 1;
            string newPermCode = $"{prefix}{newNumber:000}"; 

            return newPermCode;
        }
        public async Task AddPermission(PermissionDto permissionDto)
        {
            switch (permissionDto.PermTypeFk)
            {
                case 2:
                    await AddPermissionAsync(permissionDto);
                    break;
                case 3:
                case 4:
                    await HandleWithdrawOrDamagedPermissionAsync(permissionDto);
                    break;
                case 5:
                    await HandleTransferPermissionAsync(permissionDto);
                    break;
                default:
                    throw new Exception("Invalid permission type.");
            }
        }
        private async Task<Permission> CreatePermissionAsync(PermissionDto permissionDto)
        {
            var permissionType = await _context.PermissionTypes
                .FirstOrDefaultAsync(pt => pt.PerId == permissionDto.PermTypeFk);

            if (permissionType == null)
                throw new Exception("Permission type not found");

            string newPermCode;

            if (!string.IsNullOrWhiteSpace(permissionDto.PermCode))
            {
                var codeExists = await _context.Permissions
                    .AnyAsync(p => p.PermCode == permissionDto.PermCode);

                if (codeExists)
                {
                    throw new Exception("The provided permission code already exists.");
                }
                newPermCode = permissionDto.PermCode;
            }
            else
            {
                newPermCode = await GenerateNextPermissionCode();
            }

            var newPermission = new Permission
            {
                PermCode = newPermCode,
                SubFk = permissionDto.SubId,
                DestinationSubFk = permissionDto.DestinationSubId,
                PermTypeFk = permissionType.PerId,
                PermCreatedat = DateTime.Now
            };

            _context.Permissions.Add(newPermission);
            await _context.SaveChangesAsync();

            return newPermission;
        }
        private async Task AddPermissionAsync(PermissionDto permissionDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var newPermission = await CreatePermissionAsync(permissionDto);
                    foreach (var item in permissionDto.Items)
                    {
                        var itemExists = await _context.Items
                        .AnyAsync(i => i.ItemId == item.ItemId);

                        if (!itemExists)
                            throw new Exception($"Item not found.");

                        var itemQuantity = await _context.Quantities
                        .FirstOrDefaultAsync(q => q.ItemFk == item.ItemId);

                        if (itemQuantity != null)
                        {
                            itemQuantity.CurrentQuantity += item.Quantity;
                            itemQuantity.QuantityUpdatedat = DateTime.Now;
                        }
                        else
                        {
                            itemQuantity = new Quantity
                            {
                                ItemFk = item.ItemId,
                                CurrentQuantity = item.Quantity,
                                QuantityUpdatedat = DateTime.Now,
                                QuantityCreatedat = DateTime.Now
                            };

                            _context.Quantities.Add(itemQuantity);
                        }
                        var ItemPermission = new ItemPermission
                        {
                            ItemFk = item.ItemId,
                            PermFk = newPermission.PermId,
                            Quantity = item.Quantity
                        };

                        _context.ItemPermissions.Add(ItemPermission);

                        var subItem = await _context.SubItems
                        .FirstOrDefaultAsync(si => si.ItemFk == item.ItemId && si.SubFk == permissionDto.SubId);

                        if (subItem != null)
                        {
                            subItem.Quantity += item.Quantity;
                        }
                        else
                        {
                            subItem = new SubItem
                            {
                                ItemFk = item.ItemId,
                                SubFk = permissionDto.SubId,
                                Quantity = item.Quantity
                            };
                            _context.SubItems.Add(subItem);
                        }
                    }
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Add operation failed: {ex.Message}");
                }
            }

        }
        private async Task HandleWithdrawOrDamagedPermissionAsync(PermissionDto permissionDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var newPermission = await CreatePermissionAsync(permissionDto);
                    foreach (var item in permissionDto.Items)
                    {
                        var itemExists = await _context.Items
                            .AnyAsync(i => i.ItemId == item.ItemId);

                        if (!itemExists)
                            throw new Exception($"Item not found.");

                        var subExisted = await _context.SubWearhouses
                            .AnyAsync(s => s.SubId == permissionDto.SubId);

                        if (!subExisted)
                            throw new Exception($"Subwarehouse not found.");

                        var subItem = await _context.SubItems
                        .FirstOrDefaultAsync(si => si.ItemFk == item.ItemId && si.SubFk == permissionDto.SubId);

                        if (subItem == null || subItem.Quantity < item.Quantity)
                        {
                            throw new Exception($"Insufficient quantity in the subwarehouse for item ID {item.ItemId}.");
                        }

                        subItem.Quantity -= item.Quantity;

                        var itemQuantity = await _context.Quantities
                        .FirstOrDefaultAsync(q => q.ItemFk == item.ItemId);

                        if (itemQuantity != null && itemQuantity.CurrentQuantity >= item.Quantity)
                        {
                            itemQuantity.CurrentQuantity -= item.Quantity;
                            itemQuantity.QuantityUpdatedat = DateTime.Now;
                        }
                        else
                        {
                            throw new Exception("Insufficient quantity for item.");
                        }
                        var ItemPermission = new ItemPermission
                        {
                            ItemFk = item.ItemId,
                            PermFk = newPermission.PermId,
                            Quantity = item.Quantity
                        };
                        _context.ItemPermissions.Add(ItemPermission);

                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"{(permissionDto.PermTypeFk == 3 ? "Withdraw" : "Damaged")} operation failed: {ex.Message}");
                }
            }

        }
        private async Task HandleTransferPermissionAsync(PermissionDto permissionDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var newPermission = await CreatePermissionAsync(permissionDto);

                    foreach (var item in permissionDto.Items)
                    {
                        var itemExists = await _context.Items
                            .AnyAsync(i => i.ItemId == item.ItemId);

                        if (!itemExists)
                            throw new Exception($"Item not found.");

                        var sourceSubExists = await _context.SubWearhouses
                            .AnyAsync(s => s.SubId == permissionDto.SubId);

                        if (!sourceSubExists)
                            throw new Exception($"Source sub-warehouse not found.");

                        var destinationSubExists = await _context.SubWearhouses
                            .AnyAsync(s => s.SubId == permissionDto.DestinationSubId);

                        if (!destinationSubExists)
                            throw new Exception($"Destination sub-warehouse not found.");

                        var sourceSubItem = await _context.SubItems
                            .FirstOrDefaultAsync(si => si.ItemFk == item.ItemId && si.SubFk == permissionDto.SubId);

                        if (sourceSubItem == null || sourceSubItem.Quantity < item.Quantity)
                        {
                            throw new Exception($"Insufficient quantity in the source sub-warehouse for item ID {item.ItemId}.");
                        }

                        sourceSubItem.Quantity -= item.Quantity;

                        var destinationSubItem = await _context.SubItems
                            .FirstOrDefaultAsync(si => si.ItemFk == item.ItemId && si.SubFk == permissionDto.DestinationSubId);

                        if (destinationSubItem != null)
                        {
                            destinationSubItem.Quantity += item.Quantity;
                        }
                        else
                        {
                            destinationSubItem = new SubItem
                            {
                                ItemFk = item.ItemId,
                                SubFk = permissionDto.DestinationSubId,
                                Quantity = item.Quantity
                            };
                            _context.SubItems.Add(destinationSubItem);
                        }
                        var ItemPermission = new ItemPermission
                        {
                            ItemFk = item.ItemId,
                            PermFk = newPermission.PermId,
                            Quantity = item.Quantity
                        };
                        _context.ItemPermissions.Add(ItemPermission);
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Transfer operation failed: {ex.Message}");
                }
            }

        }
        
    }
}
