export interface Permissionaction {
  permId: number,
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
  permId: number
  permTypeFk: number
  perTypeValue: string
  subId: number
  destinationSubId: number
  items: [
    itemId: number,
    quantity: number
  ]
  itemCount: number
  permCreatedat: string
}
