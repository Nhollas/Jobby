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

eval(__webpack_require__.ts("__webpack_require__.r(__webpack_exports__);\n/* harmony export */ __webpack_require__.d(__webpack_exports__, {\n/* harmony export */   \"CreateJobModal\": function() { return /* binding */ CreateJobModal; }\n/* harmony export */ });\n/* harmony import */ var _swc_helpers_src_async_to_generator_mjs__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @swc/helpers/src/_async_to_generator.mjs */ \"./node_modules/@swc/helpers/src/_async_to_generator.mjs\");\n/* harmony import */ var _swc_helpers_src_define_property_mjs__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @swc/helpers/src/_define_property.mjs */ \"./node_modules/@swc/helpers/src/_define_property.mjs\");\n/* harmony import */ var _swc_helpers_src_object_spread_mjs__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @swc/helpers/src/_object_spread.mjs */ \"./node_modules/@swc/helpers/src/_object_spread.mjs\");\n/* harmony import */ var _swc_helpers_src_object_spread_props_mjs__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @swc/helpers/src/_object_spread_props.mjs */ \"./node_modules/@swc/helpers/src/_object_spread_props.mjs\");\n/* harmony import */ var _swc_helpers_src_to_consumable_array_mjs__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @swc/helpers/src/_to_consumable_array.mjs */ \"./node_modules/@swc/helpers/src/_to_consumable_array.mjs\");\n/* harmony import */ var _swc_helpers_src_ts_generator_mjs__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @swc/helpers/src/_ts_generator.mjs */ \"./node_modules/@swc/helpers/src/_ts_generator.mjs\");\n/* harmony import */ var react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! react/jsx-dev-runtime */ \"./node_modules/react/jsx-dev-runtime.js\");\n/* harmony import */ var react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__);\n/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! react */ \"./node_modules/react/index.js\");\n/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_1__);\n/* harmony import */ var _clients__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../clients */ \"./clients/index.ts\");\n/* harmony import */ var _reducers_CreateJobReducer__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../../reducers/CreateJobReducer */ \"./reducers/CreateJobReducer.ts\");\n/* harmony import */ var _Common__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../Common */ \"./components/Common/index.ts\");\n/* harmony import */ var _Form_Input__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../Form/Input */ \"./components/Form/Input.tsx\");\n\n\n\n\n\n\nvar _this = undefined;\n\nvar _s = $RefreshSig$();\n\n\n\n\n\nvar CreateJobModal = function(param) {\n    var setShowCreateJobModal = param.setShowCreateJobModal, showCreateJobModal = param.showCreateJobModal;\n    _s();\n    var visible = showCreateJobModal.visible, jobListId = showCreateJobModal.jobListId, boardId = showCreateJobModal.boardId, setContainerDict = showCreateJobModal.setContainerDict;\n    var ref = (0,react__WEBPACK_IMPORTED_MODULE_1__.useReducer)(_reducers_CreateJobReducer__WEBPACK_IMPORTED_MODULE_3__[\"default\"], {\n        job: {\n            title: \"\",\n            company: \"\",\n            boardId: boardId,\n            jobListId: jobListId\n        }\n    }), state = ref[0], dispatch = ref[1];\n    var handleSubmit = function() {\n        var _ref = (0,_swc_helpers_src_async_to_generator_mjs__WEBPACK_IMPORTED_MODULE_6__[\"default\"])(function(e) {\n            var createdJob;\n            return (0,_swc_helpers_src_ts_generator_mjs__WEBPACK_IMPORTED_MODULE_7__[\"default\"])(this, function(_state) {\n                switch(_state.label){\n                    case 0:\n                        e.preventDefault();\n                        return [\n                            4,\n                            _clients__WEBPACK_IMPORTED_MODULE_2__.client.post(\"/Job/Create\", state.job)\n                        ];\n                    case 1:\n                        createdJob = _state.sent();\n                        setContainerDict(function(containerDict) {\n                            return (0,_swc_helpers_src_object_spread_props_mjs__WEBPACK_IMPORTED_MODULE_8__[\"default\"])((0,_swc_helpers_src_object_spread_mjs__WEBPACK_IMPORTED_MODULE_9__[\"default\"])({}, containerDict), (0,_swc_helpers_src_define_property_mjs__WEBPACK_IMPORTED_MODULE_10__[\"default\"])({}, jobListId, (0,_swc_helpers_src_object_spread_props_mjs__WEBPACK_IMPORTED_MODULE_8__[\"default\"])((0,_swc_helpers_src_object_spread_mjs__WEBPACK_IMPORTED_MODULE_9__[\"default\"])({}, containerDict[jobListId]), {\n                                jobs: (0,_swc_helpers_src_to_consumable_array_mjs__WEBPACK_IMPORTED_MODULE_11__[\"default\"])(containerDict[jobListId].jobs).concat([\n                                    createdJob\n                                ])\n                            })));\n                        });\n                        return [\n                            2\n                        ];\n                }\n            });\n        });\n        return function handleSubmit(e) {\n            return _ref.apply(this, arguments);\n        };\n    }();\n    var handleChange = function(e) {\n        dispatch({\n            type: \"HANDLE_INPUT_CHANGE\",\n            name: e.target.name,\n            value: e.target.value\n        });\n    };\n    if (visible) {\n        return /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(_Common__WEBPACK_IMPORTED_MODULE_4__.ModalContainer, {\n            children: /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(\"form\", {\n                onSubmit: handleSubmit,\n                className: \"flex flex-col gap-y-8\",\n                method: \"post\",\n                children: [\n                    /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(\"h1\", {\n                        className: \"text-xl font-medium\",\n                        children: \"Create Job\"\n                    }, void 0, false, {\n                        fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n                        lineNumber: 74,\n                        columnNumber: 11\n                    }, _this),\n                    /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(_Form_Input__WEBPACK_IMPORTED_MODULE_5__[\"default\"], {\n                        name: \"title\",\n                        label: \"Title\",\n                        className: \"border px-3 py-1.5\",\n                        onChange: handleChange,\n                        type: \"text\",\n                        value: state.job.title\n                    }, void 0, false, {\n                        fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n                        lineNumber: 75,\n                        columnNumber: 11\n                    }, _this),\n                    /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(_Form_Input__WEBPACK_IMPORTED_MODULE_5__[\"default\"], {\n                        name: \"company\",\n                        label: \"Company\",\n                        className: \"border px-3 py-1.5\",\n                        onChange: handleChange,\n                        type: \"text\",\n                        value: state.job.company\n                    }, void 0, false, {\n                        fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n                        lineNumber: 83,\n                        columnNumber: 11\n                    }, _this),\n                    /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(_Form_Input__WEBPACK_IMPORTED_MODULE_5__[\"default\"], {\n                        name: \"boardId\",\n                        label: \"Board\",\n                        className: \"border px-3 py-1.5\",\n                        type: \"text\",\n                        value: boardId\n                    }, void 0, false, {\n                        fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n                        lineNumber: 91,\n                        columnNumber: 11\n                    }, _this),\n                    /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(_Form_Input__WEBPACK_IMPORTED_MODULE_5__[\"default\"], {\n                        name: \"jobListId\",\n                        label: \"List\",\n                        className: \"border px-3 py-1.5\",\n                        type: \"text\",\n                        value: jobListId\n                    }, void 0, false, {\n                        fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n                        lineNumber: 98,\n                        columnNumber: 11\n                    }, _this),\n                    /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(\"p\", {\n                        className: \"flex flex-row justify-center gap-4\",\n                        children: [\n                            /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(_Common__WEBPACK_IMPORTED_MODULE_4__.ActionButton, {\n                                variant: \"secondary\",\n                                text: \"Cancel\",\n                                onClick: function() {\n                                    return setShowCreateJobModal({\n                                        visible: false,\n                                        jobListId: null,\n                                        boardId: null\n                                    });\n                                }\n                            }, void 0, false, {\n                                fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n                                lineNumber: 106,\n                                columnNumber: 13\n                            }, _this),\n                            /*#__PURE__*/ (0,react_jsx_dev_runtime__WEBPACK_IMPORTED_MODULE_0__.jsxDEV)(_Common__WEBPACK_IMPORTED_MODULE_4__.ActionButton, {\n                                variant: \"primary\",\n                                text: \"Create\",\n                                type: \"submit\",\n                                extended: true\n                            }, void 0, false, {\n                                fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n                                lineNumber: 117,\n                                columnNumber: 13\n                            }, _this)\n                        ]\n                    }, void 0, true, {\n                        fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n                        lineNumber: 105,\n                        columnNumber: 11\n                    }, _this)\n                ]\n            }, void 0, true, {\n                fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n                lineNumber: 69,\n                columnNumber: 9\n            }, _this)\n        }, void 0, false, {\n            fileName: \"C:\\\\Users\\\\Nhollas\\\\Documents\\\\GitHub\\\\Jobby\\\\Jobby.Clients\\\\jobby-next\\\\components\\\\Modals\\\\Board\\\\CreateJobModal.tsx\",\n            lineNumber: 68,\n            columnNumber: 7\n        }, _this);\n    }\n};\n_s(CreateJobModal, \"AgfxqWI41OdRR+Chuv+Ua1Xx4fA=\");\n_c = CreateJobModal;\nvar _c;\n$RefreshReg$(_c, \"CreateJobModal\");\n\n\n;\n    // Wrapped in an IIFE to avoid polluting the global scope\n    ;\n    (function () {\n        var _a, _b;\n        // Legacy CSS implementations will `eval` browser code in a Node.js context\n        // to extract CSS. For backwards compatibility, we need to check we're in a\n        // browser context before continuing.\n        if (typeof self !== 'undefined' &&\n            // AMP / No-JS mode does not inject these helpers:\n            '$RefreshHelpers$' in self) {\n            // @ts-ignore __webpack_module__ is global\n            var currentExports = module.exports;\n            // @ts-ignore __webpack_module__ is global\n            var prevExports = (_b = (_a = module.hot.data) === null || _a === void 0 ? void 0 : _a.prevExports) !== null && _b !== void 0 ? _b : null;\n            // This cannot happen in MainTemplate because the exports mismatch between\n            // templating and execution.\n            self.$RefreshHelpers$.registerExportsForReactRefresh(currentExports, module.id);\n            // A module can be accepted automatically based on its exports, e.g. when\n            // it is a Refresh Boundary.\n            if (self.$RefreshHelpers$.isReactRefreshBoundary(currentExports)) {\n                // Save the previous exports on update so we can compare the boundary\n                // signatures.\n                module.hot.dispose(function (data) {\n                    data.prevExports = currentExports;\n                });\n                // Unconditionally accept an update to this module, we'll check if it's\n                // still a Refresh Boundary later.\n                // @ts-ignore importMeta is replaced in the loader\n                module.hot.accept();\n                // This field is set when the previous version of this module was a\n                // Refresh Boundary, letting us know we need to check for invalidation or\n                // enqueue an update.\n                if (prevExports !== null) {\n                    // A boundary can become ineligible if its exports are incompatible\n                    // with the previous exports.\n                    //\n                    // For example, if you add/remove/change exports, we'll want to\n                    // re-execute the importing modules, and force those components to\n                    // re-render. Similarly, if you convert a class component to a\n                    // function, we want to invalidate the boundary.\n                    if (self.$RefreshHelpers$.shouldInvalidateReactRefreshBoundary(prevExports, currentExports)) {\n                        module.hot.invalidate();\n                    }\n                    else {\n                        self.$RefreshHelpers$.scheduleUpdate();\n                    }\n                }\n            }\n            else {\n                // Since we just executed the code for the module, it's possible that the\n                // new exports made it ineligible for being a boundary.\n                // We only care about the case when we were _previously_ a boundary,\n                // because we already accepted this update (accidental side effect).\n                var isNoLongerABoundary = prevExports !== null;\n                if (isNoLongerABoundary) {\n                    module.hot.invalidate();\n                }\n            }\n        }\n    })();\n//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiLi9jb21wb25lbnRzL01vZGFscy9Cb2FyZC9DcmVhdGVKb2JNb2RhbC50c3guanMiLCJtYXBwaW5ncyI6Ijs7Ozs7Ozs7Ozs7Ozs7Ozs7O0FBQUE7Ozs7Ozs7OztBQUE2RDtBQUNuQjtBQUNlO0FBRUc7QUFDdkI7QUFtQjlCLElBQU1NLGNBQWMsR0FBRyxnQkFHakI7UUFGWEMscUJBQXFCLFNBQXJCQSxxQkFBcUIsRUFDckJDLGtCQUFrQixTQUFsQkEsa0JBQWtCOztJQUVsQixJQUFRQyxPQUFPLEdBQTJDRCxrQkFBa0IsQ0FBcEVDLE9BQU8sRUFBRUMsU0FBUyxHQUFnQ0Ysa0JBQWtCLENBQTNERSxTQUFTLEVBQUVDLE9BQU8sR0FBdUJILGtCQUFrQixDQUFoREcsT0FBTyxFQUFFQyxnQkFBZ0IsR0FBS0osa0JBQWtCLENBQXZDSSxnQkFBZ0I7SUFFckQsSUFBMEJaLEdBT3hCLEdBUHdCQSxpREFBVSxDQUFDRSxrRUFBTyxFQUFFO1FBQzVDVyxHQUFHLEVBQUU7WUFDSEMsS0FBSyxFQUFFLEVBQUU7WUFDVEMsT0FBTyxFQUFFLEVBQUU7WUFDWEosT0FBTyxFQUFQQSxPQUFPO1lBQ1BELFNBQVMsRUFBVEEsU0FBUztTQUNWO0tBQ0YsQ0FBQyxFQVBLTSxLQUFLLEdBQWNoQixHQU94QixHQVBVLEVBQUVpQixRQUFRLEdBQUlqQixHQU94QixHQVBvQjtJQVN0QixJQUFNa0IsWUFBWTttQkFBRyw2RkFBT0MsQ0FBQyxFQUFLO2dCQUUxQkMsVUFBVTs7Ozt3QkFEaEJELENBQUMsQ0FBQ0UsY0FBYyxFQUFFLENBQUM7d0JBQ0E7OzRCQUFNcEIsaURBQVcsQ0FHbEMsYUFBYSxFQUFFZSxLQUFLLENBQUNILEdBQUcsQ0FBQzswQkFBQTs7d0JBSHJCTyxVQUFVLEdBQUcsYUFHUTt3QkFFM0JSLGdCQUFnQixDQUFDLFNBQUNXLGFBQWEsRUFBSzs0QkFDbEMsT0FBTyx3S0FDRkEsYUFBYSxHQUNoQixzRkFBQ2IsU0FBUyxFQUFHLHdLQUNSYSxhQUFhLENBQUNiLFNBQVMsQ0FBQztnQ0FDM0JjLElBQUksRUFBRSxzRkFBSUQsYUFBYSxDQUFDYixTQUFTLENBQUMsQ0FBQ2MsSUFBSSxDQUE3QkQsUUFBSjtvQ0FBbUNILFVBQVU7aUNBQUM7OEJBQ3JELEVBQ0YsQ0FBQzt3QkFDSixDQUFDLENBQUMsQ0FBQzs7Ozs7O1FBQ0wsQ0FBQzt3QkFoQktGLFlBQVksQ0FBVUMsQ0FBQzs7O09BZ0I1QjtJQUVELElBQU1NLFlBQVksR0FBRyxTQUFDTixDQUFDLEVBQUs7UUFDMUJGLFFBQVEsQ0FBQztZQUNQUyxJQUFJLEVBQUUscUJBQXFCO1lBQzNCQyxJQUFJLEVBQUVSLENBQUMsQ0FBQ1MsTUFBTSxDQUFDRCxJQUFJO1lBQ25CRSxLQUFLLEVBQUVWLENBQUMsQ0FBQ1MsTUFBTSxDQUFDQyxLQUFLO1NBQ3RCLENBQUMsQ0FBQztJQUNMLENBQUM7SUFFRCxJQUFJcEIsT0FBTyxFQUFFO1FBQ1gscUJBQ0UsOERBQUNMLG1EQUFjO3NCQUNiLDRFQUFDMEIsTUFBSTtnQkFDSEMsUUFBUSxFQUFFYixZQUFZO2dCQUN0QmMsU0FBUyxFQUFDLHVCQUF1QjtnQkFDakNDLE1BQU0sRUFBQyxNQUFNOztrQ0FFYiw4REFBQ0MsSUFBRTt3QkFBQ0YsU0FBUyxFQUFDLHFCQUFxQjtrQ0FBQyxZQUFVOzs7Ozs2QkFBSztrQ0FDbkQsOERBQUMzQixtREFBSzt3QkFDSnNCLElBQUksRUFBQyxPQUFPO3dCQUNaUSxLQUFLLEVBQUMsT0FBTzt3QkFDYkgsU0FBUyxFQUFDLG9CQUFvQjt3QkFDOUJJLFFBQVEsRUFBRVgsWUFBWTt3QkFDdEJDLElBQUksRUFBQyxNQUFNO3dCQUNYRyxLQUFLLEVBQUViLEtBQUssQ0FBQ0gsR0FBRyxDQUFDQyxLQUFLOzs7Ozs2QkFDdEI7a0NBQ0YsOERBQUNULG1EQUFLO3dCQUNKc0IsSUFBSSxFQUFDLFNBQVM7d0JBQ2RRLEtBQUssRUFBQyxTQUFTO3dCQUNmSCxTQUFTLEVBQUMsb0JBQW9CO3dCQUM5QkksUUFBUSxFQUFFWCxZQUFZO3dCQUN0QkMsSUFBSSxFQUFDLE1BQU07d0JBQ1hHLEtBQUssRUFBRWIsS0FBSyxDQUFDSCxHQUFHLENBQUNFLE9BQU87Ozs7OzZCQUN4QjtrQ0FDRiw4REFBQ1YsbURBQUs7d0JBQ0pzQixJQUFJLEVBQUMsU0FBUzt3QkFDZFEsS0FBSyxFQUFDLE9BQU87d0JBQ2JILFNBQVMsRUFBQyxvQkFBb0I7d0JBQzlCTixJQUFJLEVBQUMsTUFBTTt3QkFDWEcsS0FBSyxFQUFFbEIsT0FBTzs7Ozs7NkJBQ2Q7a0NBQ0YsOERBQUNOLG1EQUFLO3dCQUNKc0IsSUFBSSxFQUFDLFdBQVc7d0JBQ2hCUSxLQUFLLEVBQUMsTUFBTTt3QkFDWkgsU0FBUyxFQUFDLG9CQUFvQjt3QkFDOUJOLElBQUksRUFBQyxNQUFNO3dCQUNYRyxLQUFLLEVBQUVuQixTQUFTOzs7Ozs2QkFDaEI7a0NBQ0YsOERBQUMyQixHQUFDO3dCQUFDTCxTQUFTLEVBQUMsb0NBQW9DOzswQ0FDL0MsOERBQUM3QixpREFBWTtnQ0FDWG1DLE9BQU8sRUFBQyxXQUFXO2dDQUNuQkMsSUFBSSxFQUFDLFFBQVE7Z0NBQ2JDLE9BQU8sRUFBRTsyQ0FDUGpDLHFCQUFxQixDQUFDO3dDQUNwQkUsT0FBTyxFQUFFLEtBQUs7d0NBQ2RDLFNBQVMsRUFBRSxJQUFJO3dDQUNmQyxPQUFPLEVBQUUsSUFBSTtxQ0FDZCxDQUFDO2lDQUFBOzs7OztxQ0FFSjswQ0FDRiw4REFBQ1IsaURBQVk7Z0NBQ1htQyxPQUFPLEVBQUMsU0FBUztnQ0FDakJDLElBQUksRUFBQyxRQUFRO2dDQUNiYixJQUFJLEVBQUMsUUFBUTtnQ0FDYmUsUUFBUTs7Ozs7cUNBQ1I7Ozs7Ozs2QkFDQTs7Ozs7O3FCQUNDOzs7OztpQkFDUSxDQUNqQjtJQUNKLENBQUM7QUFDSCxDQUFDLENBQUM7R0F2R1duQyxjQUFjO0FBQWRBLEtBQUFBLGNBQWMiLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly9fTl9FLy4vY29tcG9uZW50cy9Nb2RhbHMvQm9hcmQvQ3JlYXRlSm9iTW9kYWwudHN4P2JkYTMiXSwic291cmNlc0NvbnRlbnQiOlsiaW1wb3J0IHsgRGlzcGF0Y2gsIFNldFN0YXRlQWN0aW9uLCB1c2VSZWR1Y2VyIH0gZnJvbSBcInJlYWN0XCI7XHJcbmltcG9ydCB7IGNsaWVudCB9IGZyb20gXCIuLi8uLi8uLi9jbGllbnRzXCI7XHJcbmltcG9ydCByZWR1Y2VyIGZyb20gXCIuLi8uLi8uLi9yZWR1Y2Vycy9DcmVhdGVKb2JSZWR1Y2VyXCI7XHJcbmltcG9ydCB7IEpvYiwgSm9iTGlzdCB9IGZyb20gXCIuLi8uLi8uLi90eXBlc1wiO1xyXG5pbXBvcnQgeyBBY3Rpb25CdXR0b24sIE1vZGFsQ29udGFpbmVyIH0gZnJvbSBcIi4uLy4uL0NvbW1vblwiO1xyXG5pbXBvcnQgSW5wdXQgZnJvbSBcIi4uLy4uL0Zvcm0vSW5wdXRcIjtcclxuXHJcbmludGVyZmFjZSBQcm9wcyB7XHJcbiAgc2V0U2hvd0NyZWF0ZUpvYk1vZGFsOiBEaXNwYXRjaDxcclxuICAgIFNldFN0YXRlQWN0aW9uPHtcclxuICAgICAgdmlzaWJsZTogYm9vbGVhbjtcclxuICAgICAgYm9hcmRJZD86IHN0cmluZyB8IG51bGw7XHJcbiAgICAgIGpvYkxpc3RJZDogc3RyaW5nIHwgbnVsbDtcclxuICAgICAgc2V0Q29udGFpbmVyRGljdD86IERpc3BhdGNoPFNldFN0YXRlQWN0aW9uPFJlY29yZDxzdHJpbmcsIEpvYkxpc3Q+Pj47XHJcbiAgICB9PlxyXG4gID47XHJcbiAgc2hvd0NyZWF0ZUpvYk1vZGFsOiB7XHJcbiAgICB2aXNpYmxlOiBib29sZWFuO1xyXG4gICAgYm9hcmRJZD86IHN0cmluZyB8IG51bGw7XHJcbiAgICBqb2JMaXN0SWQ6IHN0cmluZyB8IG51bGw7XHJcbiAgICBzZXRDb250YWluZXJEaWN0PzogRGlzcGF0Y2g8U2V0U3RhdGVBY3Rpb248UmVjb3JkPHN0cmluZywgSm9iTGlzdD4+PjtcclxuICB9O1xyXG59XHJcblxyXG5leHBvcnQgY29uc3QgQ3JlYXRlSm9iTW9kYWwgPSAoe1xyXG4gIHNldFNob3dDcmVhdGVKb2JNb2RhbCxcclxuICBzaG93Q3JlYXRlSm9iTW9kYWwsXHJcbn06IFByb3BzKSA9PiB7XHJcbiAgY29uc3QgeyB2aXNpYmxlLCBqb2JMaXN0SWQsIGJvYXJkSWQsIHNldENvbnRhaW5lckRpY3QgfSA9IHNob3dDcmVhdGVKb2JNb2RhbDtcclxuXHJcbiAgY29uc3QgW3N0YXRlLCBkaXNwYXRjaF0gPSB1c2VSZWR1Y2VyKHJlZHVjZXIsIHtcclxuICAgIGpvYjoge1xyXG4gICAgICB0aXRsZTogXCJcIixcclxuICAgICAgY29tcGFueTogXCJcIixcclxuICAgICAgYm9hcmRJZCxcclxuICAgICAgam9iTGlzdElkLFxyXG4gICAgfSxcclxuICB9KTtcclxuXHJcbiAgY29uc3QgaGFuZGxlU3VibWl0ID0gYXN5bmMgKGUpID0+IHtcclxuICAgIGUucHJldmVudERlZmF1bHQoKTtcclxuICAgIGNvbnN0IGNyZWF0ZWRKb2IgPSBhd2FpdCBjbGllbnQucG9zdDxcclxuICAgICAgUGljazxKb2IsIFwiY29tcGFueVwiIHwgXCJ0aXRsZVwiIHwgXCJqb2JMaXN0SWRcIiB8IFwiYm9hcmRJZFwiPixcclxuICAgICAgSm9iXHJcbiAgICA+KFwiL0pvYi9DcmVhdGVcIiwgc3RhdGUuam9iKTtcclxuXHJcbiAgICBzZXRDb250YWluZXJEaWN0KChjb250YWluZXJEaWN0KSA9PiB7XHJcbiAgICAgIHJldHVybiB7XHJcbiAgICAgICAgLi4uY29udGFpbmVyRGljdCxcclxuICAgICAgICBbam9iTGlzdElkXToge1xyXG4gICAgICAgICAgLi4uY29udGFpbmVyRGljdFtqb2JMaXN0SWRdLFxyXG4gICAgICAgICAgam9iczogWy4uLmNvbnRhaW5lckRpY3Rbam9iTGlzdElkXS5qb2JzLCBjcmVhdGVkSm9iXSxcclxuICAgICAgICB9LFxyXG4gICAgICB9O1xyXG4gICAgfSk7XHJcbiAgfTtcclxuXHJcbiAgY29uc3QgaGFuZGxlQ2hhbmdlID0gKGUpID0+IHtcclxuICAgIGRpc3BhdGNoKHtcclxuICAgICAgdHlwZTogXCJIQU5ETEVfSU5QVVRfQ0hBTkdFXCIsXHJcbiAgICAgIG5hbWU6IGUudGFyZ2V0Lm5hbWUsXHJcbiAgICAgIHZhbHVlOiBlLnRhcmdldC52YWx1ZSxcclxuICAgIH0pO1xyXG4gIH07XHJcblxyXG4gIGlmICh2aXNpYmxlKSB7XHJcbiAgICByZXR1cm4gKFxyXG4gICAgICA8TW9kYWxDb250YWluZXI+XHJcbiAgICAgICAgPGZvcm1cclxuICAgICAgICAgIG9uU3VibWl0PXtoYW5kbGVTdWJtaXR9XHJcbiAgICAgICAgICBjbGFzc05hbWU9J2ZsZXggZmxleC1jb2wgZ2FwLXktOCdcclxuICAgICAgICAgIG1ldGhvZD0ncG9zdCdcclxuICAgICAgICA+XHJcbiAgICAgICAgICA8aDEgY2xhc3NOYW1lPSd0ZXh0LXhsIGZvbnQtbWVkaXVtJz5DcmVhdGUgSm9iPC9oMT5cclxuICAgICAgICAgIDxJbnB1dFxyXG4gICAgICAgICAgICBuYW1lPSd0aXRsZSdcclxuICAgICAgICAgICAgbGFiZWw9J1RpdGxlJ1xyXG4gICAgICAgICAgICBjbGFzc05hbWU9J2JvcmRlciBweC0zIHB5LTEuNSdcclxuICAgICAgICAgICAgb25DaGFuZ2U9e2hhbmRsZUNoYW5nZX1cclxuICAgICAgICAgICAgdHlwZT0ndGV4dCdcclxuICAgICAgICAgICAgdmFsdWU9e3N0YXRlLmpvYi50aXRsZX1cclxuICAgICAgICAgIC8+XHJcbiAgICAgICAgICA8SW5wdXRcclxuICAgICAgICAgICAgbmFtZT0nY29tcGFueSdcclxuICAgICAgICAgICAgbGFiZWw9J0NvbXBhbnknXHJcbiAgICAgICAgICAgIGNsYXNzTmFtZT0nYm9yZGVyIHB4LTMgcHktMS41J1xyXG4gICAgICAgICAgICBvbkNoYW5nZT17aGFuZGxlQ2hhbmdlfVxyXG4gICAgICAgICAgICB0eXBlPSd0ZXh0J1xyXG4gICAgICAgICAgICB2YWx1ZT17c3RhdGUuam9iLmNvbXBhbnl9XHJcbiAgICAgICAgICAvPlxyXG4gICAgICAgICAgPElucHV0XHJcbiAgICAgICAgICAgIG5hbWU9J2JvYXJkSWQnXHJcbiAgICAgICAgICAgIGxhYmVsPSdCb2FyZCdcclxuICAgICAgICAgICAgY2xhc3NOYW1lPSdib3JkZXIgcHgtMyBweS0xLjUnXHJcbiAgICAgICAgICAgIHR5cGU9J3RleHQnXHJcbiAgICAgICAgICAgIHZhbHVlPXtib2FyZElkfVxyXG4gICAgICAgICAgLz5cclxuICAgICAgICAgIDxJbnB1dFxyXG4gICAgICAgICAgICBuYW1lPSdqb2JMaXN0SWQnXHJcbiAgICAgICAgICAgIGxhYmVsPSdMaXN0J1xyXG4gICAgICAgICAgICBjbGFzc05hbWU9J2JvcmRlciBweC0zIHB5LTEuNSdcclxuICAgICAgICAgICAgdHlwZT0ndGV4dCdcclxuICAgICAgICAgICAgdmFsdWU9e2pvYkxpc3RJZH1cclxuICAgICAgICAgIC8+XHJcbiAgICAgICAgICA8cCBjbGFzc05hbWU9J2ZsZXggZmxleC1yb3cganVzdGlmeS1jZW50ZXIgZ2FwLTQnPlxyXG4gICAgICAgICAgICA8QWN0aW9uQnV0dG9uXHJcbiAgICAgICAgICAgICAgdmFyaWFudD0nc2Vjb25kYXJ5J1xyXG4gICAgICAgICAgICAgIHRleHQ9J0NhbmNlbCdcclxuICAgICAgICAgICAgICBvbkNsaWNrPXsoKSA9PlxyXG4gICAgICAgICAgICAgICAgc2V0U2hvd0NyZWF0ZUpvYk1vZGFsKHtcclxuICAgICAgICAgICAgICAgICAgdmlzaWJsZTogZmFsc2UsXHJcbiAgICAgICAgICAgICAgICAgIGpvYkxpc3RJZDogbnVsbCxcclxuICAgICAgICAgICAgICAgICAgYm9hcmRJZDogbnVsbCxcclxuICAgICAgICAgICAgICAgIH0pXHJcbiAgICAgICAgICAgICAgfVxyXG4gICAgICAgICAgICAvPlxyXG4gICAgICAgICAgICA8QWN0aW9uQnV0dG9uXHJcbiAgICAgICAgICAgICAgdmFyaWFudD0ncHJpbWFyeSdcclxuICAgICAgICAgICAgICB0ZXh0PSdDcmVhdGUnXHJcbiAgICAgICAgICAgICAgdHlwZT0nc3VibWl0J1xyXG4gICAgICAgICAgICAgIGV4dGVuZGVkXHJcbiAgICAgICAgICAgIC8+XHJcbiAgICAgICAgICA8L3A+XHJcbiAgICAgICAgPC9mb3JtPlxyXG4gICAgICA8L01vZGFsQ29udGFpbmVyPlxyXG4gICAgKTtcclxuICB9XHJcbn07XHJcbiJdLCJuYW1lcyI6WyJ1c2VSZWR1Y2VyIiwiY2xpZW50IiwicmVkdWNlciIsIkFjdGlvbkJ1dHRvbiIsIk1vZGFsQ29udGFpbmVyIiwiSW5wdXQiLCJDcmVhdGVKb2JNb2RhbCIsInNldFNob3dDcmVhdGVKb2JNb2RhbCIsInNob3dDcmVhdGVKb2JNb2RhbCIsInZpc2libGUiLCJqb2JMaXN0SWQiLCJib2FyZElkIiwic2V0Q29udGFpbmVyRGljdCIsImpvYiIsInRpdGxlIiwiY29tcGFueSIsInN0YXRlIiwiZGlzcGF0Y2giLCJoYW5kbGVTdWJtaXQiLCJlIiwiY3JlYXRlZEpvYiIsInByZXZlbnREZWZhdWx0IiwicG9zdCIsImNvbnRhaW5lckRpY3QiLCJqb2JzIiwiaGFuZGxlQ2hhbmdlIiwidHlwZSIsIm5hbWUiLCJ0YXJnZXQiLCJ2YWx1ZSIsImZvcm0iLCJvblN1Ym1pdCIsImNsYXNzTmFtZSIsIm1ldGhvZCIsImgxIiwibGFiZWwiLCJvbkNoYW5nZSIsInAiLCJ2YXJpYW50IiwidGV4dCIsIm9uQ2xpY2siLCJleHRlbmRlZCJdLCJzb3VyY2VSb290IjoiIn0=\n//# sourceURL=webpack-internal:///./components/Modals/Board/CreateJobModal.tsx\n"));

/***/ })

});