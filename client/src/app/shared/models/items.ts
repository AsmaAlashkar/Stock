export interface ItemDetailsResult {
  itemsDetails: ItemDetailsDto[];
  total: number;
}
export interface ItemDetailsDto {
  itemId: number;
  itemNameEn: string;
  itemNameAr: string;
  unitNameEn: string;
  unitNameAr: string;
  catNameEn: string;
  catNameAr: string;
  currentQuantity: number;
}
export interface Item{
  itemId: number;
  itemCode:string;
  itemNameEn: string;
  itemNameAr: string;
  catFk:number;
  uniteFk:number;
  itemExperationdate:Date;
  itemCreatedat:Date;
  itemUpdatedat:Date;
}

export interface ItemDetailsDtoVM {
  itemId: number;
  itemNameEn: string;
  itemNameAr: string;
  subWearId?: number;
  quantity?: number;
}

export interface ItemDetailsPerTab {
  itemId: number;
  itemCode: string;
  itemNameEn: string;
  itemNameAr: string;
  unitNameEn: string;
  unitNameAr: string;
  catNameEn: string;
  catNameAr: string;
  currentQuantity: number;
  quantity?: number;
}
