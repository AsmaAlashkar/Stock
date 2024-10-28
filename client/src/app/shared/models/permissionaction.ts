export interface Permissionaction {
  permId: number,
  permTypeFk: number,
  subId: number,
  destinationSubId: number,
  // items: [
  //   {
  //     "itemId": number,
  //     "quantity": number
  //   }
  // ],
  items: Array<{ itemId: number; quantity: number }>,
  permCreatedat: string
}
