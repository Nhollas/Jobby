"use strict";
/*
 * ATTENTION: An "eval-source-map" devtool has been used.
 * This devtool is neither made for production nor for readable output files.
 * It uses "eval()" calls to create a separate source file with attached SourceMaps in the browser devtools.
 * If you are trying to read the output file, select a different devtool (https://webpack.js.org/configuration/devtool/)
 * or disable the default devtool with "devtool: false".
 * If you are looking for production-ready output files, see mode: "production" (https://webpack.js.org/configuration/mode/).
 */
self["webpackHotUpdate_N_E"]("pages/board/[boardId]",{

/***/ "./reducers/CreateJobReducer.ts":
/*!**************************************!*\
  !*** ./reducers/CreateJobReducer.ts ***!
  \**************************************/
/***/ (function(module, __webpack_exports__, __webpack_require__) {

eval(__webpack_require__.ts("__webpack_require__.r(__webpack_exports__);\n/* harmony import */ var _swc_helpers_src_define_property_mjs__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @swc/helpers/src/_define_property.mjs */ \"./node_modules/@swc/helpers/src/_define_property.mjs\");\n/* harmony import */ var _swc_helpers_src_object_spread_mjs__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @swc/helpers/src/_object_spread.mjs */ \"./node_modules/@swc/helpers/src/_object_spread.mjs\");\n/* harmony import */ var _swc_helpers_src_object_spread_props_mjs__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @swc/helpers/src/_object_spread_props.mjs */ \"./node_modules/@swc/helpers/src/_object_spread_props.mjs\");\n\n\n\nvar reducer = function(state, action) {\n    var name = action.name, value = action.value;\n    switch(action.type){\n        case \"HANDLE_INPUT_CHANGE\":\n            return (0,_swc_helpers_src_object_spread_props_mjs__WEBPACK_IMPORTED_MODULE_0__[\"default\"])((0,_swc_helpers_src_object_spread_mjs__WEBPACK_IMPORTED_MODULE_1__[\"default\"])({}, state), {\n                job: (0,_swc_helpers_src_object_spread_props_mjs__WEBPACK_IMPORTED_MODULE_0__[\"default\"])((0,_swc_helpers_src_object_spread_mjs__WEBPACK_IMPORTED_MODULE_1__[\"default\"])({}, state.job), (0,_swc_helpers_src_define_property_mjs__WEBPACK_IMPORTED_MODULE_2__[\"default\"])({}, name, value))\n            });\n        default:\n            return state;\n    }\n};\n/* harmony default export */ __webpack_exports__[\"default\"] = (reducer);\n\n\n;\n    // Wrapped in an IIFE to avoid polluting the global scope\n    ;\n    (function () {\n        var _a, _b;\n        // Legacy CSS implementations will `eval` browser code in a Node.js context\n        // to extract CSS. For backwards compatibility, we need to check we're in a\n        // browser context before continuing.\n        if (typeof self !== 'undefined' &&\n            // AMP / No-JS mode does not inject these helpers:\n            '$RefreshHelpers$' in self) {\n            // @ts-ignore __webpack_module__ is global\n            var currentExports = module.exports;\n            // @ts-ignore __webpack_module__ is global\n            var prevExports = (_b = (_a = module.hot.data) === null || _a === void 0 ? void 0 : _a.prevExports) !== null && _b !== void 0 ? _b : null;\n            // This cannot happen in MainTemplate because the exports mismatch between\n            // templating and execution.\n            self.$RefreshHelpers$.registerExportsForReactRefresh(currentExports, module.id);\n            // A module can be accepted automatically based on its exports, e.g. when\n            // it is a Refresh Boundary.\n            if (self.$RefreshHelpers$.isReactRefreshBoundary(currentExports)) {\n                // Save the previous exports on update so we can compare the boundary\n                // signatures.\n                module.hot.dispose(function (data) {\n                    data.prevExports = currentExports;\n                });\n                // Unconditionally accept an update to this module, we'll check if it's\n                // still a Refresh Boundary later.\n                // @ts-ignore importMeta is replaced in the loader\n                module.hot.accept();\n                // This field is set when the previous version of this module was a\n                // Refresh Boundary, letting us know we need to check for invalidation or\n                // enqueue an update.\n                if (prevExports !== null) {\n                    // A boundary can become ineligible if its exports are incompatible\n                    // with the previous exports.\n                    //\n                    // For example, if you add/remove/change exports, we'll want to\n                    // re-execute the importing modules, and force those components to\n                    // re-render. Similarly, if you convert a class component to a\n                    // function, we want to invalidate the boundary.\n                    if (self.$RefreshHelpers$.shouldInvalidateReactRefreshBoundary(prevExports, currentExports)) {\n                        module.hot.invalidate();\n                    }\n                    else {\n                        self.$RefreshHelpers$.scheduleUpdate();\n                    }\n                }\n            }\n            else {\n                // Since we just executed the code for the module, it's possible that the\n                // new exports made it ineligible for being a boundary.\n                // We only care about the case when we were _previously_ a boundary,\n                // because we already accepted this update (accidental side effect).\n                var isNoLongerABoundary = prevExports !== null;\n                if (isNoLongerABoundary) {\n                    module.hot.invalidate();\n                }\n            }\n        }\n    })();\n//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiLi9yZWR1Y2Vycy9DcmVhdGVKb2JSZWR1Y2VyLnRzLmpzIiwibWFwcGluZ3MiOiI7Ozs7QUFBQTs7O0FBYUEsSUFBTUEsT0FBTyxHQUEyQixTQUFDQyxLQUFLLEVBQUVDLE1BQU0sRUFBSztJQUN6RCxJQUFRQyxJQUFJLEdBQVlELE1BQU0sQ0FBdEJDLElBQUksRUFBRUMsS0FBSyxHQUFLRixNQUFNLENBQWhCRSxLQUFLO0lBQ25CLE9BQVFGLE1BQU0sQ0FBQ0csSUFBSTtRQUNqQixLQUFLLHFCQUFxQjtZQUN4QixPQUFPLHdLQUNGSixLQUFLO2dCQUNSSyxHQUFHLEVBQUUsd0tBQ0FMLEtBQUssQ0FBQ0ssR0FBRyxHQUNaLHFGQUFDSCxJQUFJLEVBQUdDLEtBQUssRUFDZDtjQUNGLENBQUM7UUFDSjtZQUNFLE9BQU9ILEtBQUssQ0FBQztLQUNoQjtBQUNILENBQUM7QUFFRCwrREFBZUQsT0FBTyxFQUFDIiwic291cmNlcyI6WyJ3ZWJwYWNrOi8vX05fRS8uL3JlZHVjZXJzL0NyZWF0ZUpvYlJlZHVjZXIudHM/NzI4ZiJdLCJzb3VyY2VzQ29udGVudCI6WyJpbXBvcnQgeyBSZWR1Y2VyIH0gZnJvbSAncmVhY3QnO1xyXG5pbXBvcnQgeyBKb2IgfSBmcm9tICcuLi90eXBlcyc7XHJcblxyXG5pbnRlcmZhY2UgQWN0aW9uIHtcclxuICB0eXBlOiBzdHJpbmc7XHJcbiAgbmFtZTogc3RyaW5nO1xyXG4gIHZhbHVlOiBhbnk7XHJcbn1cclxuXHJcbmludGVyZmFjZSBTdGF0ZSB7XHJcbiAgam9iOiBQaWNrPEpvYiwgXCJjb21wYW55XCIgfCBcInRpdGxlXCIgfCBcImpvYkxpc3RJZFwiIHwgXCJib2FyZElkXCI+O1xyXG59XHJcblxyXG5jb25zdCByZWR1Y2VyOiBSZWR1Y2VyPFN0YXRlLCBBY3Rpb24+ID0gKHN0YXRlLCBhY3Rpb24pID0+IHtcclxuICBjb25zdCB7IG5hbWUsIHZhbHVlIH0gPSBhY3Rpb247XHJcbiAgc3dpdGNoIChhY3Rpb24udHlwZSkge1xyXG4gICAgY2FzZSAnSEFORExFX0lOUFVUX0NIQU5HRSc6XHJcbiAgICAgIHJldHVybiB7XHJcbiAgICAgICAgLi4uc3RhdGUsXHJcbiAgICAgICAgam9iOiB7XHJcbiAgICAgICAgICAuLi5zdGF0ZS5qb2IsXHJcbiAgICAgICAgICBbbmFtZV06IHZhbHVlLFxyXG4gICAgICAgIH1cclxuICAgICAgfTtcclxuICAgIGRlZmF1bHQ6XHJcbiAgICAgIHJldHVybiBzdGF0ZTtcclxuICB9XHJcbn07XHJcblxyXG5leHBvcnQgZGVmYXVsdCByZWR1Y2VyO1xyXG4iXSwibmFtZXMiOlsicmVkdWNlciIsInN0YXRlIiwiYWN0aW9uIiwibmFtZSIsInZhbHVlIiwidHlwZSIsImpvYiJdLCJzb3VyY2VSb290IjoiIn0=\n//# sourceURL=webpack-internal:///./reducers/CreateJobReducer.ts\n"));

/***/ })

});