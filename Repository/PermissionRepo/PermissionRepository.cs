using Microsoft.EntityFrameworkCore;
using Standard.DTOs;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PermissionRepo
{
    public class PermissionRepository:IPermissionRepository
    {
        private readonly StockContext _context;

        public PermissionRepository(StockContext context)
        {
            _context = context;
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
                default:
                    throw new Exception("Invalid permission type.");
            }
        }
        private async Task<Permission> CreatePermissionAsync(int permissionTypeFk)
        {
            var permissionType = await _context.PermissionTypes
                .FirstOrDefaultAsync(pt => pt.PerId == permissionTypeFk);

            if (permissionType == null)
                throw new Exception("Permission type not found");

            var newPermission = new Permission
            {
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
                    var newPermission = await CreatePermissionAsync(permissionDto.PermTypeFk);
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
                                QuantityUpdatedat = DateTime.Now
                            };
                            _context.Quantities.Add(itemQuantity);
                        }
                        var itemPermission = new ItemPermission
                        {
                            ItemFk = item.ItemId,
                            PermFk = newPermission.PermId,
                            Quantity = item.Quantity
                        };
                        _context.ItemPermissions.Add(itemPermission);
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
                    var newPermission = await CreatePermissionAsync(permissionDto.PermTypeFk);
                    foreach (var item in permissionDto.Items)
                    {
                        var itemExists = await _context.Items
                        .AnyAsync(i => i.ItemId == item.ItemId);

                        if (!itemExists)
                            throw new Exception($"Item not found.");

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
                        var itemPermission = new ItemPermission
                        {
                            ItemFk = item.ItemId,
                            PermFk = newPermission.PermId,
                            Quantity = item.Quantity
                        };
                        _context.ItemPermissions.Add(itemPermission);
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
    }
}
