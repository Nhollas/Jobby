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

/***/ "./components/Form/Input.tsx":
/*!***********************************!*\
  !*** ./components/Form/Input.tsx ***!
  \***********************************/
/***/ (function(module, __webpack_exports__, __webpack_require__) {

eval(__webpack_require__.ts("__webpack_require__.r(__webpack_exports__);\n/* harmony import */ var react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! react/jsx-dev-runtime */ \"./node_modules/react/jsx-dev-runtime.js\");\n/* harmony import */ var react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__);\nvar _this = undefined;\n\nvar Input = function(props) {\n    var type = props.type, className = props.className, onChange = props.onChange, placeholder = props.placeholder, value = props.value, name = props.name, checked = props.checked, label = props.label;\n    var formattedClassName = className ? className : \"px-3 py-1 border border-gray-300\";\n    if (type === \"textarea\") {\n        return /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(\"textarea\", {\n            className: formattedClassName,\n            onChange: onChange,\n            placeholder: placeholder,\n            value: value,\n            name: name\n        }, void 0, false, {\n            fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Form\\\\Input.tsx\",\n            lineNumber: 30,\n            columnNumber: 7\n        }, _this);\n    } else {\n        if (label !== undefined) {\n            return /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(\"div\", {\n                className: \"flex flex-col justify-center gap-y-1.5\",\n                children: [\n                    label !== undefined && /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(\"label\", {\n                        className: \"text-sm font-medium\",\n                        htmlFor: name,\n                        children: label\n                    }, void 0, false, {\n                        fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Form\\\\Input.tsx\",\n                        lineNumber: 43,\n                        columnNumber: 13\n                    }, _this),\n                    /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(\"input\", {\n                        type: type,\n                        className: formattedClassName,\n                        onChange: onChange,\n                        readOnly: onChange ? false : true,\n                        disabled: onChange ? false : true,\n                        placeholder: placeholder,\n                        value: value,\n                        name: name,\n                        checked: checked\n                    }, void 0, false, {\n                        fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Form\\\\Input.tsx\",\n                        lineNumber: 47,\n                        columnNumber: 11\n                    }, _this)\n                ]\n            }, void 0, true, {\n                fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Form\\\\Input.tsx\",\n                lineNumber: 41,\n                columnNumber: 9\n            }, _this);\n        } else {\n            return /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(\"input\", {\n                type: type,\n                className: formattedClassName,\n                onChange: onChange,\n                readOnly: onChange ? false : true,\n                disabled: onChange ? false : true,\n                placeholder: placeholder,\n                value: value,\n                name: name,\n                checked: checked\n            }, void 0, false, {\n                fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Form\\\\Input.tsx\",\n                lineNumber: 62,\n                columnNumber: 9\n            }, _this);\n        }\n    }\n};\n_c = Input;\n/* harmony default export */ __webpack_exports__[\"default\"] = (Input);\nvar _c;\n$RefreshReg$(_c, \"Input\");\n\n\n;\n    // Wrapped in an IIFE to avoid polluting the global scope\n    ;\n    (function () {\n        var _a, _b;\n        // Legacy CSS implementations will `eval` browser code in a Node.js context\n        // to extract CSS. For backwards compatibility, we need to check we're in a\n        // browser context before continuing.\n        if (typeof self !== 'undefined' &&\n            // AMP / No-JS mode does not inject these helpers:\n            '$RefreshHelpers$' in self) {\n            // @ts-ignore __webpack_module__ is global\n            var currentExports = module.exports;\n            // @ts-ignore __webpack_module__ is global\n            var prevExports = (_b = (_a = module.hot.data) === null || _a === void 0 ? void 0 : _a.prevExports) !== null && _b !== void 0 ? _b : null;\n            // This cannot happen in MainTemplate because the exports mismatch between\n            // templating and execution.\n            self.$RefreshHelpers$.registerExportsForReactRefresh(currentExports, module.id);\n            // A module can be accepted automatically based on its exports, e.g. when\n            // it is a Refresh Boundary.\n            if (self.$RefreshHelpers$.isReactRefreshBoundary(currentExports)) {\n                // Save the previous exports on update so we can compare the boundary\n                // signatures.\n                module.hot.dispose(function (data) {\n                    data.prevExports = currentExports;\n                });\n                // Unconditionally accept an update to this module, we'll check if it's\n                // still a Refresh Boundary later.\n                // @ts-ignore importMeta is replaced in the loader\n                module.hot.accept();\n                // This field is set when the previous version of this module was a\n                // Refresh Boundary, letting us know we need to check for invalidation or\n                // enqueue an update.\n                if (prevExports !== null) {\n                    // A boundary can become ineligible if its exports are incompatible\n                    // with the previous exports.\n                    //\n                    // For example, if you add/remove/change exports, we'll want to\n                    // re-execute the importing modules, and force those components to\n                    // re-render. Similarly, if you convert a class component to a\n                    // function, we want to invalidate the boundary.\n                    if (self.$RefreshHelpers$.shouldInvalidateReactRefreshBoundary(prevExports, currentExports)) {\n                        module.hot.invalidate();\n                    }\n                    else {\n                        self.$RefreshHelpers$.scheduleUpdate();\n                    }\n                }\n            }\n            else {\n                // Since we just executed the code for the module, it's possible that the\n                // new exports made it ineligible for being a boundary.\n                // We only care about the case when we were _previously_ a boundary,\n                // because we already accepted this update (accidental side effect).\n                var isNoLongerABoundary = prevExports !== null;\n                if (isNoLongerABoundary) {\n                    module.hot.invalidate();\n                }\n            }\n        }\n    })();\n//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiLi9jb21wb25lbnRzL0Zvcm0vSW5wdXQudHN4LmpzIiwibWFwcGluZ3MiOiI7OztBQUFBOztBQVdBLElBQU1BLEtBQUssR0FBRyxTQUFDQyxLQUFZLEVBQUs7SUFDOUIsSUFDRUMsSUFBSSxHQVFGRCxLQUFLLENBUlBDLElBQUksRUFDSkMsU0FBUyxHQU9QRixLQUFLLENBUFBFLFNBQVMsRUFDVEMsUUFBUSxHQU1OSCxLQUFLLENBTlBHLFFBQVEsRUFDUkMsV0FBVyxHQUtUSixLQUFLLENBTFBJLFdBQVcsRUFDWEMsS0FBSyxHQUlITCxLQUFLLENBSlBLLEtBQUssRUFDTEMsSUFBSSxHQUdGTixLQUFLLENBSFBNLElBQUksRUFDSkMsT0FBTyxHQUVMUCxLQUFLLENBRlBPLE9BQU8sRUFDUEMsS0FBSyxHQUNIUixLQUFLLENBRFBRLEtBQUs7SUFHUCxJQUFJQyxrQkFBa0IsR0FBR1AsU0FBUyxHQUM5QkEsU0FBUyxHQUNULGtDQUFrQztJQUV0QyxJQUFJRCxJQUFJLEtBQUssVUFBVSxFQUFFO1FBQ3ZCLHFCQUNFLDhEQUFDUyxVQUFRO1lBQ1BSLFNBQVMsRUFBRU8sa0JBQWtCO1lBQzdCTixRQUFRLEVBQUVBLFFBQVE7WUFDbEJDLFdBQVcsRUFBRUEsV0FBVztZQUN4QkMsS0FBSyxFQUFFQSxLQUFLO1lBQ1pDLElBQUksRUFBRUEsSUFBSTs7Ozs7aUJBQ0EsQ0FDWjtJQUNKLE9BQU87UUFDTCxJQUFJRSxLQUFLLEtBQUtHLFNBQVMsRUFBRTtZQUN2QixxQkFDRSw4REFBQ0MsS0FBRztnQkFBQ1YsU0FBUyxFQUFDLHdDQUF3Qzs7b0JBQ3BETSxLQUFLLEtBQUtHLFNBQVMsa0JBQ2xCLDhEQUFDSCxPQUFLO3dCQUFDTixTQUFTLEVBQUMscUJBQXFCO3dCQUFDVyxPQUFPLEVBQUVQLElBQUk7a0NBQ2pERSxLQUFLOzs7Ozs2QkFDQTtrQ0FFViw4REFBQ00sT0FBSzt3QkFDSmIsSUFBSSxFQUFFQSxJQUFJO3dCQUNWQyxTQUFTLEVBQUVPLGtCQUFrQjt3QkFDN0JOLFFBQVEsRUFBRUEsUUFBUTt3QkFDbEJZLFFBQVEsRUFBRVosUUFBUSxHQUFHLEtBQUssR0FBRyxJQUFJO3dCQUNqQ2EsUUFBUSxFQUFFYixRQUFRLEdBQUcsS0FBSyxHQUFHLElBQUk7d0JBQ2pDQyxXQUFXLEVBQUVBLFdBQVc7d0JBQ3hCQyxLQUFLLEVBQUVBLEtBQUs7d0JBQ1pDLElBQUksRUFBRUEsSUFBSTt3QkFDVkMsT0FBTyxFQUFFQSxPQUFPOzs7Ozs2QkFDVDs7Ozs7O3FCQUNMLENBQ047UUFDSixPQUFPO1lBQ0wscUJBQ0UsOERBQUNPLE9BQUs7Z0JBQ0piLElBQUksRUFBRUEsSUFBSTtnQkFDVkMsU0FBUyxFQUFFTyxrQkFBa0I7Z0JBQzdCTixRQUFRLEVBQUVBLFFBQVE7Z0JBQ2xCWSxRQUFRLEVBQUVaLFFBQVEsR0FBRyxLQUFLLEdBQUcsSUFBSTtnQkFDakNhLFFBQVEsRUFBRWIsUUFBUSxHQUFHLEtBQUssR0FBRyxJQUFJO2dCQUNqQ0MsV0FBVyxFQUFFQSxXQUFXO2dCQUN4QkMsS0FBSyxFQUFFQSxLQUFLO2dCQUNaQyxJQUFJLEVBQUVBLElBQUk7Z0JBQ1ZDLE9BQU8sRUFBRUEsT0FBTzs7Ozs7cUJBQ1QsQ0FDVDtRQUNKLENBQUM7SUFDSCxDQUFDO0FBQ0gsQ0FBQztBQWhFS1IsS0FBQUEsS0FBSztBQWtFWCwrREFBZUEsS0FBSyxFQUFDIiwic291cmNlcyI6WyJ3ZWJwYWNrOi8vX05fRS8uL2NvbXBvbmVudHMvRm9ybS9JbnB1dC50c3g/OGZlYiJdLCJzb3VyY2VzQ29udGVudCI6WyJpbnRlcmZhY2UgUHJvcHMge1xyXG4gIHR5cGU6IHN0cmluZztcclxuICBjbGFzc05hbWU6IHN0cmluZztcclxuICBvbkNoYW5nZT86ICh2YWx1ZSkgPT4gdm9pZDtcclxuICBwbGFjZWhvbGRlcj86IHN0cmluZztcclxuICBuYW1lOiBzdHJpbmc7XHJcbiAgdmFsdWU6IHN0cmluZyB8IG51bWJlcjtcclxuICBjaGVja2VkPzogYm9vbGVhbjtcclxuICBsYWJlbDogc3RyaW5nO1xyXG59XHJcblxyXG5jb25zdCBJbnB1dCA9IChwcm9wczogUHJvcHMpID0+IHtcclxuICBjb25zdCB7XHJcbiAgICB0eXBlLFxyXG4gICAgY2xhc3NOYW1lLFxyXG4gICAgb25DaGFuZ2UsXHJcbiAgICBwbGFjZWhvbGRlcixcclxuICAgIHZhbHVlLFxyXG4gICAgbmFtZSxcclxuICAgIGNoZWNrZWQsXHJcbiAgICBsYWJlbCxcclxuICB9ID0gcHJvcHM7XHJcblxyXG4gIGxldCBmb3JtYXR0ZWRDbGFzc05hbWUgPSBjbGFzc05hbWVcclxuICAgID8gY2xhc3NOYW1lXHJcbiAgICA6IFwicHgtMyBweS0xIGJvcmRlciBib3JkZXItZ3JheS0zMDBcIjtcclxuXHJcbiAgaWYgKHR5cGUgPT09IFwidGV4dGFyZWFcIikge1xyXG4gICAgcmV0dXJuIChcclxuICAgICAgPHRleHRhcmVhXHJcbiAgICAgICAgY2xhc3NOYW1lPXtmb3JtYXR0ZWRDbGFzc05hbWV9XHJcbiAgICAgICAgb25DaGFuZ2U9e29uQ2hhbmdlfVxyXG4gICAgICAgIHBsYWNlaG9sZGVyPXtwbGFjZWhvbGRlcn1cclxuICAgICAgICB2YWx1ZT17dmFsdWV9XHJcbiAgICAgICAgbmFtZT17bmFtZX1cclxuICAgICAgPjwvdGV4dGFyZWE+XHJcbiAgICApO1xyXG4gIH0gZWxzZSB7XHJcbiAgICBpZiAobGFiZWwgIT09IHVuZGVmaW5lZCkge1xyXG4gICAgICByZXR1cm4gKFxyXG4gICAgICAgIDxkaXYgY2xhc3NOYW1lPSdmbGV4IGZsZXgtY29sIGp1c3RpZnktY2VudGVyIGdhcC15LTEuNSc+XHJcbiAgICAgICAgICB7bGFiZWwgIT09IHVuZGVmaW5lZCAmJiAoXHJcbiAgICAgICAgICAgIDxsYWJlbCBjbGFzc05hbWU9J3RleHQtc20gZm9udC1tZWRpdW0nIGh0bWxGb3I9e25hbWV9PlxyXG4gICAgICAgICAgICAgIHtsYWJlbH1cclxuICAgICAgICAgICAgPC9sYWJlbD5cclxuICAgICAgICAgICl9XHJcbiAgICAgICAgICA8aW5wdXRcclxuICAgICAgICAgICAgdHlwZT17dHlwZX1cclxuICAgICAgICAgICAgY2xhc3NOYW1lPXtmb3JtYXR0ZWRDbGFzc05hbWV9XHJcbiAgICAgICAgICAgIG9uQ2hhbmdlPXtvbkNoYW5nZX1cclxuICAgICAgICAgICAgcmVhZE9ubHk9e29uQ2hhbmdlID8gZmFsc2UgOiB0cnVlfVxyXG4gICAgICAgICAgICBkaXNhYmxlZD17b25DaGFuZ2UgPyBmYWxzZSA6IHRydWV9XHJcbiAgICAgICAgICAgIHBsYWNlaG9sZGVyPXtwbGFjZWhvbGRlcn1cclxuICAgICAgICAgICAgdmFsdWU9e3ZhbHVlfVxyXG4gICAgICAgICAgICBuYW1lPXtuYW1lfVxyXG4gICAgICAgICAgICBjaGVja2VkPXtjaGVja2VkfVxyXG4gICAgICAgICAgPjwvaW5wdXQ+XHJcbiAgICAgICAgPC9kaXY+XHJcbiAgICAgICk7XHJcbiAgICB9IGVsc2Uge1xyXG4gICAgICByZXR1cm4gKFxyXG4gICAgICAgIDxpbnB1dFxyXG4gICAgICAgICAgdHlwZT17dHlwZX1cclxuICAgICAgICAgIGNsYXNzTmFtZT17Zm9ybWF0dGVkQ2xhc3NOYW1lfVxyXG4gICAgICAgICAgb25DaGFuZ2U9e29uQ2hhbmdlfVxyXG4gICAgICAgICAgcmVhZE9ubHk9e29uQ2hhbmdlID8gZmFsc2UgOiB0cnVlfVxyXG4gICAgICAgICAgZGlzYWJsZWQ9e29uQ2hhbmdlID8gZmFsc2UgOiB0cnVlfVxyXG4gICAgICAgICAgcGxhY2Vob2xkZXI9e3BsYWNlaG9sZGVyfVxyXG4gICAgICAgICAgdmFsdWU9e3ZhbHVlfVxyXG4gICAgICAgICAgbmFtZT17bmFtZX1cclxuICAgICAgICAgIGNoZWNrZWQ9e2NoZWNrZWR9XHJcbiAgICAgICAgPjwvaW5wdXQ+XHJcbiAgICAgICk7XHJcbiAgICB9XHJcbiAgfVxyXG59O1xyXG5cclxuZXhwb3J0IGRlZmF1bHQgSW5wdXQ7XHJcbiJdLCJuYW1lcyI6WyJJbnB1dCIsInByb3BzIiwidHlwZSIsImNsYXNzTmFtZSIsIm9uQ2hhbmdlIiwicGxhY2Vob2xkZXIiLCJ2YWx1ZSIsIm5hbWUiLCJjaGVja2VkIiwibGFiZWwiLCJmb3JtYXR0ZWRDbGFzc05hbWUiLCJ0ZXh0YXJlYSIsInVuZGVmaW5lZCIsImRpdiIsImh0bWxGb3IiLCJpbnB1dCIsInJlYWRPbmx5IiwiZGlzYWJsZWQiXSwic291cmNlUm9vdCI6IiJ9\n//# sourceURL=webpack-internal:///./components/Form/Input.tsx\n"));

/***/ })

});