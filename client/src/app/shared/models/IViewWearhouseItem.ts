export interface IViewWearhouseItem {
  mainId: number
  mainName: string
  mainDescription: string
  mainAdderess: string
  mainCreatedat: string
  mainUpdatedat: string
  subId: number
  subName: string
  subAddress: string
  subDescription: string
  subCreatedat: string
  subUpdatedat: string
  parentSubWearhouseId: number
  level: number
  itemId: number
  itemName: string
  itemExperationdate: string
  md: boolean
  sd: boolean
  id: boolean
  children?: IViewWearhouseItem[];  // Recursive structure for sub-warehouses
  }


 