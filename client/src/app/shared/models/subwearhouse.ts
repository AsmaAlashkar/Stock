export interface ISubWearhouse {
    subId: number
    mainFk: number
    parentSubWearhouseId: number
    subName: string
    subDescription: string
    subAddress: string
    subCreatedat: string
    subUpdatedat: string
    delet: boolean
  }

export interface Column {
  field: string;
  header: string;
}
