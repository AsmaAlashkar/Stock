export interface Category {
    catId: number;
    catNameAr:string;
    parentCategoryId:number;
    level:number;
    children?: Category[];
}
