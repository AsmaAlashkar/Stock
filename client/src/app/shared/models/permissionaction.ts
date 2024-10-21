export interface Permissionaction {
  "permId": number,
  "permTypeFk": number,
  "items": [
    {
      "itemId": number,
      "subId": number,
      "destinationSubId": number,
      "quantity": number,
    }
  ],
  "permCreatedat": string
}
