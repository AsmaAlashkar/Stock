export interface IViewWearhouseItem {
  mainId: number
  mainName: string
  mainDescription: string
  mainAdderess: string
  mainCreatedat: string
  mainUpdatedat: string
  subId: any
  subName: any
  subAddress: any
  subDescription: any
  subCreatedat: any
  subUpdatedat: any
  parentSubWearhouseId: any
  level: any
  itemId: any
  itemName: any
  itemExperationdate: any
  md: boolean
  sd: any
  id: any
  children?: IViewWearhouseItem[];  // Recursive structure for sub-warehouses
  }