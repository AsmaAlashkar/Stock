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
