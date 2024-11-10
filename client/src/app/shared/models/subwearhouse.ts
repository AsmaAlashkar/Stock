export interface ISubWearhouse {
    subId: number
    mainFk: number
    parentSubWearhouseId: number
    subNameEn: string
    subNameAr: string
    subDescriptionEn: string
    subDescriptionAr: string
    subAddressEn: string
    subAddressAr: string
    subCreatedat: string
    subUpdatedat: string
    delet: boolean
  }

export interface Column {
  field: string;
  header: string;
}

export interface subWearhouseVM {
  subId: number;
  subName: string;
}
