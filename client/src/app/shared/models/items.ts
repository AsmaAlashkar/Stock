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
  itemNameEn: string,
  itemNameAr: string,
  catFk:number,
  uniteFk:number,
  itemExperationdate:Date,
  itemCreatedat:Date,
  itemUpdatedat:Date
}

export interface ItemDetailsDtoVM {
  itemId: number,
  itemName: string,
  subWearId?: number,
  quantity?: number;
}

export interface ItemDetailsPerTab {
  itemId: number
  itemName: string
  itemCode: string
  unitName: string
  categoryName: string
  currentQuantity: number
  quantity?: number;
}
