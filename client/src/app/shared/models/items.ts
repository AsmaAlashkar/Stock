export interface ItemDetailsResult {
  itemsDetails: ItemDetailsDto[],
  total: number,
}

export interface ItemDetailsDto {
  itemId: number,
  itemName: string,
  unitName: string,
  categoryName: string,
  currentQuantity: number
}
