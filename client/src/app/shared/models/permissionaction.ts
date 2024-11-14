export interface Permissionaction {
  permId: number,
  permCode :string,
  permTypeFk: number,
  subId: number,
  destinationSubId?: number | null,
  // items: [
  //   {
  //     "itemId": number,
  //     "quantity": number
  //   }
  // ],
  items: { itemId: number; quantity: number }[];
  permCreatedat: string
}

export interface DisplayAllPermission {
  values: DisplayAllPermissionVM[]
  totalRecords: number
  totalPages: number
  pageNumber: number
  pageSize: number
}
export interface DisplayAllPermissionVM {
  permId: number,
  permCode: string,
  permTypeFk: number,
  perTypeValueAr: string,
  perTypeValueEn: string,
  subNameEn: string,
  subNameAr: string,
  destinationSubNameEn: string | null,
  destinationSubNameAr: string | null,
  itemCount: number,
  permCreatedat: string
}
