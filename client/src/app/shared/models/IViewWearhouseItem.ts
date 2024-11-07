export interface IViewWearhouseItem {
  mainId: number
  mainNameEn: string
  mainNameAr: string
  mainDescriptionEn: string
  mainDescriptionAr: string
  mainAdderess: string
  mainCreatedat: string
  mainUpdatedat: string
  subId: number
  subNameEn: string
  subNameAr: string
  subAddressEn: string
  subAddressAr: string
  subCreatedat: string
  subUpdatedat: string
  subDescriptionEn: string
  subDescriptionAr: string
  parentSubWearhouseId: number
  level: number
  itemId: number
  itemNameEn: string
  itemNameAr: string
  itemExperationdate: string
  md: boolean
  sd: boolean
  id: boolean
  children?: IViewWearhouseItem[];  // Recursive structure for sub-warehouses
  }


 