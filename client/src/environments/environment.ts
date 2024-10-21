// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
    production: false,
    apiUrl: 'http://localhost:5050/api/',

    getMainWearhouse: 'http://localhost:5050/api/MainWearhouse/GetMainWearhouse',
    getMainWearhouseById:'http://localhost:5050/api/MainWearhouse/GetMainWearhouseById/',

    getSubwarehouse: 'http://localhost:5050/api/SubWearhouse/GetSubWearhouse',

    getCtegories:'http://localhost:5050/api/Category/GetCategories',
    getCategoryById:'http://localhost:5050/api/Category/GetCategoryById/',
    createCtegory:'http://localhost:5050/api/Category/CreateCategory',
    updateCategory:'http://localhost:5050/api/Category/UpdateCategory/',

    getItems:'http://localhost:5050/api/Item/GetItems',
    getItemsVM:'http://localhost:5050/api/Item/GetItemNames',
    getItemsByCategoryId:'http://localhost:5050/api/Item/GetItemsByCategoryId/',
    getItemsBySubId:'http://localhost:5050/api/Item/GetItemsBySubWHId/',
    createItem:'http://localhost:5050/api/Item/CreateItem',
    
    getUnitsNames:'http://localhost:5050/api/Unit/GetUnitsNames',

    getpermissiontype: 'http://localhost:5050/api/PermissionType/GetAllPermissionTypes',
    permissionAction: 'http://localhost:5050/api/Permission/CreatePermission'
  };

  /*
   * For easier debugging in development mode, you can import the following file
   * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
   *
   * This import should be commented out in production mode because it will have a negative impact
   * on performance if an error is thrown.
   */
  // import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
