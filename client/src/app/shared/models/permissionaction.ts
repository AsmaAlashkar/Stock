export interface Permissionaction {
  "permId": number,
  "permTypeFk": number,
  "items": [
    {
      "itemId": number,
      "itemName": "string",
      "catFk": number,
      "uniteFk": number,
      "subFk": number,
      "itemExperationdate": "string",
      "itemCreatedat": "string",
      "itemUpdatedat": "string",
      "quantity": number
    }
  ],
  "permCreatedat": "string"
}
