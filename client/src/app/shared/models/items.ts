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
export interface Item{
  itemId: number,
  itemCode:string,
  itemName: string,
  catFk:number,
  uniteFk:number,
  itemExperationdate:Date,
  itemCreatedat:Date,
  itemUpdatedat:Date
}
