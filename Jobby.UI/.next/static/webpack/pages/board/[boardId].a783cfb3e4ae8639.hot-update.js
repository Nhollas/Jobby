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

/***/ "./components/Modals/Board/CreateJobModal.tsx":
/*!****************************************************!*\
  !*** ./components/Modals/Board/CreateJobModal.tsx ***!
  \****************************************************/
/***/ (function(module, __webpack_exports__, __webpack_require__) {

eval(__webpack_require__.ts("__webpack_require__.r(__webpack_exports__);\n/* harmony export */ __webpack_require__.d(__webpack_exports__, {\n/* harmony export */   \"CreateJobModal\": function() { return /* binding */ CreateJobModal; }\n/* harmony export */ });\n/* harmony import */ var react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! react/jsx-dev-runtime */ \"./node_modules/react/jsx-dev-runtime.js\");\n/* harmony import */ var react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__);\n/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! react */ \"./node_modules/react/index.js\");\n/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_1__);\n/* harmony import */ var _reducers_CreateJobReducer__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../reducers/CreateJobReducer */ \"./reducers/CreateJobReducer.ts\");\n/* harmony import */ var _Common__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../Common */ \"./components/Common/index.ts\");\n/* harmony import */ var _Form_Input__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../Form/Input */ \"./components/Form/Input.tsx\");\nvar _this = undefined;\n\nvar _s = $RefreshSig$();\n\n\n\n\nvar CreateJobModal = function(param) {\n    var setShowCreateJobModal = param.setShowCreateJobModal, showCreateJobModal = param.showCreateJobModal;\n    _s();\n    var ref = (0,react__WEBPACK_IMPORTED_MODULE_1__.useReducer)(_reducers_CreateJobReducer__WEBPACK_IMPORTED_MODULE_2__[\"default\"], {\n        job: {\n            title: \"\"\n        }\n    }), state = ref[0], dispatch = ref[1];\n    var visible = showCreateJobModal.visible, jobListId = showCreateJobModal.jobListId, boardId = showCreateJobModal.boardId, setContainerDict = showCreateJobModal.setContainerDict;\n    var handleSubmit = function() {\n        console.log(state);\n    };\n    var handleChange = function(e) {\n        dispatch({\n            type: \"HANDLE_INPUT_CHANGE\",\n            name: e.target.name,\n            value: e.target.value\n        });\n    };\n    if (visible) {\n        return /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(_Common__WEBPACK_IMPORTED_MODULE_3__.ModalContainer, {\n            children: /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(\"form\", {\n                onSubmit: handleSubmit,\n                className: \"flex flex-col gap-y-8\",\n                method: \"post\",\n                children: [\n                    /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(\"h1\", {\n                        className: \"text-xl font-medium\",\n                        children: \"Create Job\"\n                    }, void 0, false, {\n                        fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n                        lineNumber: 60,\n                        columnNumber: 11\n                    }, _this),\n                    /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(_Form_Input__WEBPACK_IMPORTED_MODULE_4__[\"default\"], {\n                        name: \"title\",\n                        label: \"Title\",\n                        className: \"border px-3 py-1.5\",\n                        onChange: handleChange,\n                        type: \"text\",\n                        value: state.job.title\n                    }, void 0, false, {\n                        fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n                        lineNumber: 61,\n                        columnNumber: 11\n                    }, _this),\n                    /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(_Form_Input__WEBPACK_IMPORTED_MODULE_4__[\"default\"], {\n                        name: \"company\",\n                        label: \"Company\",\n                        className: \"border px-3 py-1.5\",\n                        onChange: handleChange,\n                        type: \"text\",\n                        value: state.job.company\n                    }, void 0, false, {\n                        fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n                        lineNumber: 69,\n                        columnNumber: 11\n                    }, _this),\n                    /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(_Form_Input__WEBPACK_IMPORTED_MODULE_4__[\"default\"], {\n                        name: \"boardId\",\n                        label: \"Board\",\n                        className: \"border px-3 py-1.5\",\n                        type: \"text\",\n                        value: boardId\n                    }, void 0, false, {\n                        fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n                        lineNumber: 77,\n                        columnNumber: 11\n                    }, _this),\n                    /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(_Form_Input__WEBPACK_IMPORTED_MODULE_4__[\"default\"], {\n                        name: \"jobListId\",\n                        label: \"List\",\n                        className: \"border px-3 py-1.5\",\n                        type: \"text\",\n                        value: jobListId\n                    }, void 0, false, {\n                        fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n                        lineNumber: 84,\n                        columnNumber: 11\n                    }, _this),\n                    /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(\"p\", {\n                        className: \"flex flex-row justify-center gap-4\",\n                        children: [\n                            /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(_Common__WEBPACK_IMPORTED_MODULE_3__.ActionButton, {\n                                variant: \"secondary\",\n                                text: \"Cancel\",\n                                onClick: function() {\n                                    return setShowCreateJobModal({\n                                        visible: false,\n                                        jobListId: null,\n                                        boardId: null\n                                    });\n                                }\n                            }, void 0, false, {\n                                fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n                                lineNumber: 92,\n                                columnNumber: 13\n                            }, _this),\n                            /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(_Common__WEBPACK_IMPORTED_MODULE_3__.ActionButton, {\n                                variant: \"primary\",\n                                text: \"Create\",\n                                type: \"submit\",\n                                extended: true\n                            }, void 0, false, {\n                                fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n                                lineNumber: 103,\n                                columnNumber: 13\n                            }, _this)\n                        ]\n                    }, void 0, true, {\n                        fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n                        lineNumber: 91,\n                        columnNumber: 11\n                    }, _this)\n                ]\n            }, void 0, true, {\n                fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n                lineNumber: 55,\n                columnNumber: 9\n            }, _this)\n        }, void 0, false, {\n            fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n            lineNumber: 54,\n            columnNumber: 7\n        }, _this);\n    }\n};\n_s(CreateJobModal, \"SoMs/6Abdn+HBwdyYADmFGhJlWs=\");\n_c = CreateJobModal;\nvar _c;\n$RefreshReg$(_c, \"CreateJobModal\");\n\n\n;\n    // Wrapped in an IIFE to avoid polluting the global scope\n    ;\n    (function () {\n        var _a, _b;\n        // Legacy CSS implementations will `eval` browser code in a Node.js context\n        // to extract CSS. For backwards compatibility, we need to check we're in a\n        // browser context before continuing.\n        if (typeof self !== 'undefined' &&\n            // AMP / No-JS mode does not inject these helpers:\n            '$RefreshHelpers$' in self) {\n            // @ts-ignore __webpack_module__ is global\n            var currentExports = module.exports;\n            // @ts-ignore __webpack_module__ is global\n            var prevExports = (_b = (_a = module.hot.data) === null || _a === void 0 ? void 0 : _a.prevExports) !== null && _b !== void 0 ? _b : null;\n            // This cannot happen in MainTemplate because the exports mismatch between\n            // templating and execution.\n            self.$RefreshHelpers$.registerExportsForReactRefresh(currentExports, module.id);\n            // A module can be accepted automatically based on its exports, e.g. when\n            // it is a Refresh Boundary.\n            if (self.$RefreshHelpers$.isReactRefreshBoundary(currentExports)) {\n                // Save the previous exports on update so we can compare the boundary\n                // signatures.\n                module.hot.dispose(function (data) {\n                    data.prevExports = currentExports;\n                });\n                // Unconditionally accept an update to this module, we'll check if it's\n                // still a Refresh Boundary later.\n                // @ts-ignore importMeta is replaced in the loader\n                module.hot.accept();\n                // This field is set when the previous version of this module was a\n                // Refresh Boundary, letting us know we need to check for invalidation or\n                // enqueue an update.\n                if (prevExports !== null) {\n                    // A boundary can become ineligible if its exports are incompatible\n                    // with the previous exports.\n                    //\n                    // For example, if you add/remove/change exports, we'll want to\n                    // re-execute the importing modules, and force those components to\n                    // re-render. Similarly, if you convert a class component to a\n                    // function, we want to invalidate the boundary.\n                    if (self.$RefreshHelpers$.shouldInvalidateReactRefreshBoundary(prevExports, currentExports)) {\n                        module.hot.invalidate();\n                    }\n                    else {\n                        self.$RefreshHelpers$.scheduleUpdate();\n                    }\n                }\n            }\n            else {\n                // Since we just executed the code for the module, it's possible that the\n                // new exports made it ineligible for being a boundary.\n                // We only care about the case when we were _previously_ a boundary,\n                // because we already accepted this update (accidental side effect).\n                var isNoLongerABoundary = prevExports !== null;\n                if (isNoLongerABoundary) {\n                    module.hot.invalidate();\n                }\n            }\n        }\n    })();\n//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiLi9jb21wb25lbnRzL01vZGFscy9Cb2FyZC9DcmVhdGVKb2JNb2RhbC50c3guanMiLCJtYXBwaW5ncyI6Ijs7Ozs7Ozs7Ozs7QUFBQTs7O0FBTWU7QUFDMEM7QUFFRztBQUN2QjtBQW1COUIsSUFBTUssY0FBYyxHQUFHLGdCQUdqQjtRQUZYQyxxQkFBcUIsU0FBckJBLHFCQUFxQixFQUNyQkMsa0JBQWtCLFNBQWxCQSxrQkFBa0I7O0lBRWxCLElBQTBCUCxHQUV4QixHQUZ3QkEsaURBQVUsQ0FBQ0Msa0VBQU8sRUFBRTtRQUM1Q08sR0FBRyxFQUFFO1lBQUVDLEtBQUssRUFBRSxFQUFFO1NBQUU7S0FDbkIsQ0FBQyxFQUZLQyxLQUFLLEdBQWNWLEdBRXhCLEdBRlUsRUFBRVcsUUFBUSxHQUFJWCxHQUV4QixHQUZvQjtJQUl0QixJQUFRWSxPQUFPLEdBQTJDTCxrQkFBa0IsQ0FBcEVLLE9BQU8sRUFBRUMsU0FBUyxHQUFnQ04sa0JBQWtCLENBQTNETSxTQUFTLEVBQUVDLE9BQU8sR0FBdUJQLGtCQUFrQixDQUFoRE8sT0FBTyxFQUFFQyxnQkFBZ0IsR0FBS1Isa0JBQWtCLENBQXZDUSxnQkFBZ0I7SUFFckQsSUFBTUMsWUFBWSxHQUFHLFdBQU07UUFDekJDLE9BQU8sQ0FBQ0MsR0FBRyxDQUFDUixLQUFLLENBQUMsQ0FBQztJQUNyQixDQUFDO0lBRUQsSUFBTVMsWUFBWSxHQUFHLFNBQUNDLENBQUMsRUFBSztRQUMxQlQsUUFBUSxDQUFDO1lBQ1BVLElBQUksRUFBRSxxQkFBcUI7WUFDM0JDLElBQUksRUFBRUYsQ0FBQyxDQUFDRyxNQUFNLENBQUNELElBQUk7WUFDbkJFLEtBQUssRUFBRUosQ0FBQyxDQUFDRyxNQUFNLENBQUNDLEtBQUs7U0FDdEIsQ0FBQyxDQUFDO0lBQ0wsQ0FBQztJQUVELElBQUlaLE9BQU8sRUFBRTtRQUNYLHFCQUNFLDhEQUFDVCxtREFBYztzQkFDYiw0RUFBQ3NCLE1BQUk7Z0JBQ0hDLFFBQVEsRUFBRVYsWUFBWTtnQkFDdEJXLFNBQVMsRUFBQyx1QkFBdUI7Z0JBQ2pDQyxNQUFNLEVBQUMsTUFBTTs7a0NBRWIsOERBQUNDLElBQUU7d0JBQUNGLFNBQVMsRUFBQyxxQkFBcUI7a0NBQUMsWUFBVTs7Ozs7NkJBQUs7a0NBQ25ELDhEQUFDdkIsbURBQUs7d0JBQ0prQixJQUFJLEVBQUMsT0FBTzt3QkFDWlEsS0FBSyxFQUFDLE9BQU87d0JBQ2JILFNBQVMsRUFBQyxvQkFBb0I7d0JBQzlCSSxRQUFRLEVBQUVaLFlBQVk7d0JBQ3RCRSxJQUFJLEVBQUMsTUFBTTt3QkFDWEcsS0FBSyxFQUFFZCxLQUFLLENBQUNGLEdBQUcsQ0FBQ0MsS0FBSzs7Ozs7NkJBQ3RCO2tDQUNGLDhEQUFDTCxtREFBSzt3QkFDSmtCLElBQUksRUFBQyxTQUFTO3dCQUNkUSxLQUFLLEVBQUMsU0FBUzt3QkFDZkgsU0FBUyxFQUFDLG9CQUFvQjt3QkFDOUJJLFFBQVEsRUFBRVosWUFBWTt3QkFDdEJFLElBQUksRUFBQyxNQUFNO3dCQUNYRyxLQUFLLEVBQUVkLEtBQUssQ0FBQ0YsR0FBRyxDQUFDd0IsT0FBTzs7Ozs7NkJBQ3hCO2tDQUNGLDhEQUFDNUIsbURBQUs7d0JBQ0prQixJQUFJLEVBQUMsU0FBUzt3QkFDZFEsS0FBSyxFQUFDLE9BQU87d0JBQ2JILFNBQVMsRUFBQyxvQkFBb0I7d0JBQzlCTixJQUFJLEVBQUMsTUFBTTt3QkFDWEcsS0FBSyxFQUFFVixPQUFPOzs7Ozs2QkFDZDtrQ0FDRiw4REFBQ1YsbURBQUs7d0JBQ0prQixJQUFJLEVBQUMsV0FBVzt3QkFDaEJRLEtBQUssRUFBQyxNQUFNO3dCQUNaSCxTQUFTLEVBQUMsb0JBQW9CO3dCQUM5Qk4sSUFBSSxFQUFDLE1BQU07d0JBQ1hHLEtBQUssRUFBRVgsU0FBUzs7Ozs7NkJBQ2hCO2tDQUNGLDhEQUFDb0IsR0FBQzt3QkFBQ04sU0FBUyxFQUFDLG9DQUFvQzs7MENBQy9DLDhEQUFDekIsaURBQVk7Z0NBQ1hnQyxPQUFPLEVBQUMsV0FBVztnQ0FDbkJDLElBQUksRUFBQyxRQUFRO2dDQUNiQyxPQUFPLEVBQUU7MkNBQ1A5QixxQkFBcUIsQ0FBQzt3Q0FDcEJNLE9BQU8sRUFBRSxLQUFLO3dDQUNkQyxTQUFTLEVBQUUsSUFBSTt3Q0FDZkMsT0FBTyxFQUFFLElBQUk7cUNBQ2QsQ0FBQztpQ0FBQTs7Ozs7cUNBRUo7MENBQ0YsOERBQUNaLGlEQUFZO2dDQUNYZ0MsT0FBTyxFQUFDLFNBQVM7Z0NBQ2pCQyxJQUFJLEVBQUMsUUFBUTtnQ0FDYmQsSUFBSSxFQUFDLFFBQVE7Z0NBQ2JnQixRQUFROzs7OztxQ0FDUjs7Ozs7OzZCQUNBOzs7Ozs7cUJBQ0M7Ozs7O2lCQUNRLENBQ2pCO0lBQ0osQ0FBQztBQUNILENBQUMsQ0FBQztHQXBGV2hDLGNBQWM7QUFBZEEsS0FBQUEsY0FBYyIsInNvdXJjZXMiOlsid2VicGFjazovL19OX0UvLi9jb21wb25lbnRzL01vZGFscy9Cb2FyZC9DcmVhdGVKb2JNb2RhbC50c3g/YmRhMyJdLCJzb3VyY2VzQ29udGVudCI6WyJpbXBvcnQge1xyXG4gIERpc3BhdGNoLFxyXG4gIFJlZHVjZXJBY3Rpb24sXHJcbiAgUmVkdWNlclN0YXRlLFxyXG4gIFNldFN0YXRlQWN0aW9uLFxyXG4gIHVzZVJlZHVjZXIsXHJcbn0gZnJvbSBcInJlYWN0XCI7XHJcbmltcG9ydCByZWR1Y2VyIGZyb20gXCIuLi8uLi8uLi9yZWR1Y2Vycy9DcmVhdGVKb2JSZWR1Y2VyXCI7XHJcbmltcG9ydCB7IEpvYiwgSm9iTGlzdCB9IGZyb20gXCIuLi8uLi8uLi90eXBlc1wiO1xyXG5pbXBvcnQgeyBBY3Rpb25CdXR0b24sIE1vZGFsQ29udGFpbmVyIH0gZnJvbSBcIi4uLy4uL0NvbW1vblwiO1xyXG5pbXBvcnQgSW5wdXQgZnJvbSBcIi4uLy4uL0Zvcm0vSW5wdXRcIjtcclxuXHJcbmludGVyZmFjZSBQcm9wcyB7XHJcbiAgc2V0U2hvd0NyZWF0ZUpvYk1vZGFsOiBEaXNwYXRjaDxcclxuICAgIFNldFN0YXRlQWN0aW9uPHtcclxuICAgICAgdmlzaWJsZTogYm9vbGVhbjtcclxuICAgICAgYm9hcmRJZD86IHN0cmluZyB8IG51bGw7XHJcbiAgICAgIGpvYkxpc3RJZDogc3RyaW5nIHwgbnVsbDtcclxuICAgICAgc2V0Q29udGFpbmVyRGljdD86IERpc3BhdGNoPFNldFN0YXRlQWN0aW9uPFJlY29yZDxzdHJpbmcsIEpvYkxpc3Q+Pj47XHJcbiAgICB9PlxyXG4gID47XHJcbiAgc2hvd0NyZWF0ZUpvYk1vZGFsOiB7XHJcbiAgICB2aXNpYmxlOiBib29sZWFuO1xyXG4gICAgYm9hcmRJZD86IHN0cmluZyB8IG51bGw7XHJcbiAgICBqb2JMaXN0SWQ6IHN0cmluZyB8IG51bGw7XHJcbiAgICBzZXRDb250YWluZXJEaWN0PzogRGlzcGF0Y2g8U2V0U3RhdGVBY3Rpb248UmVjb3JkPHN0cmluZywgSm9iTGlzdD4+PjtcclxuICB9O1xyXG59XHJcblxyXG5leHBvcnQgY29uc3QgQ3JlYXRlSm9iTW9kYWwgPSAoe1xyXG4gIHNldFNob3dDcmVhdGVKb2JNb2RhbCxcclxuICBzaG93Q3JlYXRlSm9iTW9kYWwsXHJcbn06IFByb3BzKSA9PiB7XHJcbiAgY29uc3QgW3N0YXRlLCBkaXNwYXRjaF0gPSB1c2VSZWR1Y2VyKHJlZHVjZXIsIHtcclxuICAgIGpvYjogeyB0aXRsZTogXCJcIiB9LFxyXG4gIH0pO1xyXG5cclxuICBjb25zdCB7IHZpc2libGUsIGpvYkxpc3RJZCwgYm9hcmRJZCwgc2V0Q29udGFpbmVyRGljdCB9ID0gc2hvd0NyZWF0ZUpvYk1vZGFsO1xyXG5cclxuICBjb25zdCBoYW5kbGVTdWJtaXQgPSAoKSA9PiB7XHJcbiAgICBjb25zb2xlLmxvZyhzdGF0ZSk7XHJcbiAgfTtcclxuXHJcbiAgY29uc3QgaGFuZGxlQ2hhbmdlID0gKGUpID0+IHtcclxuICAgIGRpc3BhdGNoKHtcclxuICAgICAgdHlwZTogXCJIQU5ETEVfSU5QVVRfQ0hBTkdFXCIsXHJcbiAgICAgIG5hbWU6IGUudGFyZ2V0Lm5hbWUsXHJcbiAgICAgIHZhbHVlOiBlLnRhcmdldC52YWx1ZSxcclxuICAgIH0pO1xyXG4gIH07XHJcblxyXG4gIGlmICh2aXNpYmxlKSB7XHJcbiAgICByZXR1cm4gKFxyXG4gICAgICA8TW9kYWxDb250YWluZXI+XHJcbiAgICAgICAgPGZvcm1cclxuICAgICAgICAgIG9uU3VibWl0PXtoYW5kbGVTdWJtaXR9XHJcbiAgICAgICAgICBjbGFzc05hbWU9J2ZsZXggZmxleC1jb2wgZ2FwLXktOCdcclxuICAgICAgICAgIG1ldGhvZD0ncG9zdCdcclxuICAgICAgICA+XHJcbiAgICAgICAgICA8aDEgY2xhc3NOYW1lPSd0ZXh0LXhsIGZvbnQtbWVkaXVtJz5DcmVhdGUgSm9iPC9oMT5cclxuICAgICAgICAgIDxJbnB1dFxyXG4gICAgICAgICAgICBuYW1lPSd0aXRsZSdcclxuICAgICAgICAgICAgbGFiZWw9J1RpdGxlJ1xyXG4gICAgICAgICAgICBjbGFzc05hbWU9J2JvcmRlciBweC0zIHB5LTEuNSdcclxuICAgICAgICAgICAgb25DaGFuZ2U9e2hhbmRsZUNoYW5nZX1cclxuICAgICAgICAgICAgdHlwZT0ndGV4dCdcclxuICAgICAgICAgICAgdmFsdWU9e3N0YXRlLmpvYi50aXRsZX1cclxuICAgICAgICAgIC8+XHJcbiAgICAgICAgICA8SW5wdXRcclxuICAgICAgICAgICAgbmFtZT0nY29tcGFueSdcclxuICAgICAgICAgICAgbGFiZWw9J0NvbXBhbnknXHJcbiAgICAgICAgICAgIGNsYXNzTmFtZT0nYm9yZGVyIHB4LTMgcHktMS41J1xyXG4gICAgICAgICAgICBvbkNoYW5nZT17aGFuZGxlQ2hhbmdlfVxyXG4gICAgICAgICAgICB0eXBlPSd0ZXh0J1xyXG4gICAgICAgICAgICB2YWx1ZT17c3RhdGUuam9iLmNvbXBhbnl9XHJcbiAgICAgICAgICAvPlxyXG4gICAgICAgICAgPElucHV0XHJcbiAgICAgICAgICAgIG5hbWU9J2JvYXJkSWQnXHJcbiAgICAgICAgICAgIGxhYmVsPSdCb2FyZCdcclxuICAgICAgICAgICAgY2xhc3NOYW1lPSdib3JkZXIgcHgtMyBweS0xLjUnXHJcbiAgICAgICAgICAgIHR5cGU9J3RleHQnXHJcbiAgICAgICAgICAgIHZhbHVlPXtib2FyZElkfVxyXG4gICAgICAgICAgLz5cclxuICAgICAgICAgIDxJbnB1dFxyXG4gICAgICAgICAgICBuYW1lPSdqb2JMaXN0SWQnXHJcbiAgICAgICAgICAgIGxhYmVsPSdMaXN0J1xyXG4gICAgICAgICAgICBjbGFzc05hbWU9J2JvcmRlciBweC0zIHB5LTEuNSdcclxuICAgICAgICAgICAgdHlwZT0ndGV4dCdcclxuICAgICAgICAgICAgdmFsdWU9e2pvYkxpc3RJZH1cclxuICAgICAgICAgIC8+XHJcbiAgICAgICAgICA8cCBjbGFzc05hbWU9J2ZsZXggZmxleC1yb3cganVzdGlmeS1jZW50ZXIgZ2FwLTQnPlxyXG4gICAgICAgICAgICA8QWN0aW9uQnV0dG9uXHJcbiAgICAgICAgICAgICAgdmFyaWFudD0nc2Vjb25kYXJ5J1xyXG4gICAgICAgICAgICAgIHRleHQ9J0NhbmNlbCdcclxuICAgICAgICAgICAgICBvbkNsaWNrPXsoKSA9PlxyXG4gICAgICAgICAgICAgICAgc2V0U2hvd0NyZWF0ZUpvYk1vZGFsKHtcclxuICAgICAgICAgICAgICAgICAgdmlzaWJsZTogZmFsc2UsXHJcbiAgICAgICAgICAgICAgICAgIGpvYkxpc3RJZDogbnVsbCxcclxuICAgICAgICAgICAgICAgICAgYm9hcmRJZDogbnVsbCxcclxuICAgICAgICAgICAgICAgIH0pXHJcbiAgICAgICAgICAgICAgfVxyXG4gICAgICAgICAgICAvPlxyXG4gICAgICAgICAgICA8QWN0aW9uQnV0dG9uXHJcbiAgICAgICAgICAgICAgdmFyaWFudD0ncHJpbWFyeSdcclxuICAgICAgICAgICAgICB0ZXh0PSdDcmVhdGUnXHJcbiAgICAgICAgICAgICAgdHlwZT0nc3VibWl0J1xyXG4gICAgICAgICAgICAgIGV4dGVuZGVkXHJcbiAgICAgICAgICAgIC8+XHJcbiAgICAgICAgICA8L3A+XHJcbiAgICAgICAgPC9mb3JtPlxyXG4gICAgICA8L01vZGFsQ29udGFpbmVyPlxyXG4gICAgKTtcclxuICB9XHJcbn07XHJcbiJdLCJuYW1lcyI6WyJ1c2VSZWR1Y2VyIiwicmVkdWNlciIsIkFjdGlvbkJ1dHRvbiIsIk1vZGFsQ29udGFpbmVyIiwiSW5wdXQiLCJDcmVhdGVKb2JNb2RhbCIsInNldFNob3dDcmVhdGVKb2JNb2RhbCIsInNob3dDcmVhdGVKb2JNb2RhbCIsImpvYiIsInRpdGxlIiwic3RhdGUiLCJkaXNwYXRjaCIsInZpc2libGUiLCJqb2JMaXN0SWQiLCJib2FyZElkIiwic2V0Q29udGFpbmVyRGljdCIsImhhbmRsZVN1Ym1pdCIsImNvbnNvbGUiLCJsb2ciLCJoYW5kbGVDaGFuZ2UiLCJlIiwidHlwZSIsIm5hbWUiLCJ0YXJnZXQiLCJ2YWx1ZSIsImZvcm0iLCJvblN1Ym1pdCIsImNsYXNzTmFtZSIsIm1ldGhvZCIsImgxIiwibGFiZWwiLCJvbkNoYW5nZSIsImNvbXBhbnkiLCJwIiwidmFyaWFudCIsInRleHQiLCJvbkNsaWNrIiwiZXh0ZW5kZWQiXSwic291cmNlUm9vdCI6IiJ9\n//# sourceURL=webpack-internal:///./components/Modals/Board/CreateJobModal.tsx\n"));

/***/ })

});