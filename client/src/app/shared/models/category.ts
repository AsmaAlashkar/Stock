export interface Category {
    catId: number
    parentCategoryId: number
    catNameAr: string
    catNameEn: string
    catDesAr: string
    catDesEn: string
    level: number
    children?: Category[];
}
export interface Column {
    field: string;
    header: string;
  }
