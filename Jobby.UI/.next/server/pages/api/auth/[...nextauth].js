"use strict";
/*
 * ATTENTION: An "eval-source-map" devtool has been used.
 * This devtool is neither made for production nor for readable output files.
 * It uses "eval()" calls to create a separate source file with attached SourceMaps in the browser devtools.
 * If you are trying to read the output file, select a different devtool (https://webpack.js.org/configuration/devtool/)
 * or disable the default devtool with "devtool: false".
 * If you are looking for production-ready output files, see mode: "production" (https://webpack.js.org/configuration/mode/).
 */
(() => {
var exports = {};
exports.id = "pages/api/auth/[...nextauth]";
exports.ids = ["pages/api/auth/[...nextauth]"];
exports.modules = {

/***/ "next-auth":
/*!****************************!*\
  !*** external "next-auth" ***!
  \****************************/
/***/ ((module) => {

module.exports = require("next-auth");

/***/ }),

/***/ "next-auth/providers/credentials":
/*!**************************************************!*\
  !*** external "next-auth/providers/credentials" ***!
  \**************************************************/
/***/ ((module) => {

module.exports = require("next-auth/providers/credentials");

/***/ }),

/***/ "https":
/*!************************!*\
  !*** external "https" ***!
  \************************/
/***/ ((module) => {

module.exports = require("https");

/***/ }),

/***/ "(api)/./pages/api/auth/[...nextauth].ts":
/*!*****************************************!*\
  !*** ./pages/api/auth/[...nextauth].ts ***!
  \*****************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export */ __webpack_require__.d(__webpack_exports__, {\n/* harmony export */   \"authOptions\": () => (/* binding */ authOptions),\n/* harmony export */   \"default\": () => (__WEBPACK_DEFAULT_EXPORT__)\n/* harmony export */ });\n/* harmony import */ var next_auth__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! next-auth */ \"next-auth\");\n/* harmony import */ var next_auth__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(next_auth__WEBPACK_IMPORTED_MODULE_0__);\n/* harmony import */ var next_auth_providers_credentials__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! next-auth/providers/credentials */ \"next-auth/providers/credentials\");\n/* harmony import */ var next_auth_providers_credentials__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(next_auth_providers_credentials__WEBPACK_IMPORTED_MODULE_1__);\n\n\nconst authOptions = {\n    secret: process.env.NEXTAUTH_SECRET,\n    providers: [\n        next_auth_providers_credentials__WEBPACK_IMPORTED_MODULE_1___default()({\n            name: \"Credentials\",\n            credentials: {\n                username: {\n                    label: \"Username\",\n                    type: \"text\",\n                    placeholder: \"jsmith\"\n                },\n                password: {\n                    label: \"Password\",\n                    type: \"password\"\n                }\n            },\n            async authorize (credentials, req) {\n                const { username , password  } = credentials;\n                const https = __webpack_require__(/*! https */ \"https\");\n                const agent = new https.Agent({\n                    rejectUnauthorized: false\n                });\n                const options = {\n                    method: \"POST\",\n                    headers: {\n                        \"Content-Type\": \"application/json\"\n                    },\n                    agent,\n                    body: JSON.stringify({\n                        username,\n                        password\n                    })\n                };\n                const res = await fetch(`https://localhost:6001/api/auth/login`, options);\n                const user = await res.json();\n                if (res.ok && user) {\n                    return user;\n                } else return null;\n            }\n        }), \n    ],\n    session: {\n        strategy: \"jwt\"\n    },\n    pages: {\n        signIn: \"/login\"\n    },\n    callbacks: {\n        async jwt ({ token , user  }) {\n            if (user) {\n                token.accessToken = user.accessToken;\n            }\n            return token;\n        },\n        async session ({ session , token  }) {\n            console.log(session);\n            session.accessToken = token.accessToken;\n            return session;\n        }\n    }\n};\n/* harmony default export */ const __WEBPACK_DEFAULT_EXPORT__ = (next_auth__WEBPACK_IMPORTED_MODULE_0___default()(authOptions));\n//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiKGFwaSkvLi9wYWdlcy9hcGkvYXV0aC9bLi4ubmV4dGF1dGhdLnRzLmpzIiwibWFwcGluZ3MiOiI7Ozs7Ozs7OztBQUFrRDtBQUNnQjtBQUMzRCxNQUFNRSxXQUFXLEdBQWlCO0lBQ3ZDQyxNQUFNLEVBQUVDLE9BQU8sQ0FBQ0MsR0FBRyxDQUFDQyxlQUFlO0lBQ25DQyxTQUFTLEVBQUU7UUFDVE4sc0VBQW1CLENBQUM7WUFDbEJPLElBQUksRUFBRSxhQUFhO1lBQ25CQyxXQUFXLEVBQUU7Z0JBQ1hDLFFBQVEsRUFBRTtvQkFBRUMsS0FBSyxFQUFFLFVBQVU7b0JBQUVDLElBQUksRUFBRSxNQUFNO29CQUFFQyxXQUFXLEVBQUUsUUFBUTtpQkFBRTtnQkFDcEVDLFFBQVEsRUFBRTtvQkFBRUgsS0FBSyxFQUFFLFVBQVU7b0JBQUVDLElBQUksRUFBRSxVQUFVO2lCQUFFO2FBQ2xEO1lBQ0QsTUFBTUcsU0FBUyxFQUFDTixXQUFXLEVBQUVPLEdBQUcsRUFBRTtnQkFDaEMsTUFBTSxFQUFFTixRQUFRLEdBQUVJLFFBQVEsR0FBRSxHQUFHTCxXQUFXO2dCQUUxQyxNQUFNUSxLQUFLLEdBQUdDLG1CQUFPLENBQUMsb0JBQU8sQ0FBQztnQkFDOUIsTUFBTUMsS0FBSyxHQUFHLElBQUlGLEtBQUssQ0FBQ0csS0FBSyxDQUFDO29CQUM1QkMsa0JBQWtCLEVBQUUsS0FBSztpQkFDMUIsQ0FBQztnQkFFRixNQUFNQyxPQUFPLEdBQUc7b0JBQ2RDLE1BQU0sRUFBRSxNQUFNO29CQUNkQyxPQUFPLEVBQUU7d0JBQ1AsY0FBYyxFQUFFLGtCQUFrQjtxQkFDbkM7b0JBQ0RMLEtBQUs7b0JBQ0xNLElBQUksRUFBRUMsSUFBSSxDQUFDQyxTQUFTLENBQUM7d0JBQ25CakIsUUFBUTt3QkFDUkksUUFBUTtxQkFDVCxDQUFDO2lCQUNIO2dCQUVELE1BQU1jLEdBQUcsR0FBRyxNQUFNQyxLQUFLLENBQ3JCLENBQUMscUNBQXFDLENBQUMsRUFDdkNQLE9BQU8sQ0FDUjtnQkFFRCxNQUFNUSxJQUFJLEdBQUcsTUFBTUYsR0FBRyxDQUFDRyxJQUFJLEVBQUU7Z0JBRTdCLElBQUlILEdBQUcsQ0FBQ0ksRUFBRSxJQUFJRixJQUFJLEVBQUU7b0JBQ2xCLE9BQU9BLElBQUksQ0FBQztnQkFDZCxPQUFPLE9BQU8sSUFBSSxDQUFDO1lBQ3JCLENBQUM7U0FDRixDQUFDO0tBQ0g7SUFDREcsT0FBTyxFQUFFO1FBQ1BDLFFBQVEsRUFBRSxLQUFLO0tBQ2hCO0lBQ0RDLEtBQUssRUFBRTtRQUNMQyxNQUFNLEVBQUUsUUFBUTtLQUNqQjtJQUNEQyxTQUFTLEVBQUU7UUFDVCxNQUFNQyxHQUFHLEVBQUMsRUFBRUMsS0FBSyxHQUFFVCxJQUFJLEdBQUUsRUFBRTtZQUN6QixJQUFJQSxJQUFJLEVBQUU7Z0JBQ1JTLEtBQUssQ0FBQ0MsV0FBVyxHQUFHVixJQUFJLENBQUNVLFdBQVcsQ0FBQztZQUN2QyxDQUFDO1lBQ0QsT0FBT0QsS0FBSyxDQUFDO1FBQ2YsQ0FBQztRQUNELE1BQU1OLE9BQU8sRUFBQyxFQUFFQSxPQUFPLEdBQUVNLEtBQUssR0FBRSxFQUFFO1lBQ2hDRSxPQUFPLENBQUNDLEdBQUcsQ0FBQ1QsT0FBTyxDQUFDLENBQUM7WUFFckJBLE9BQU8sQ0FBQ08sV0FBVyxHQUFHRCxLQUFLLENBQUNDLFdBQVcsQ0FBQztZQUV4QyxPQUFPUCxPQUFPLENBQUM7UUFDakIsQ0FBQztLQUNGO0NBQ0YsQ0FBQztBQUNGLGlFQUFlakMsZ0RBQVEsQ0FBQ0UsV0FBVyxDQUFDLEVBQUMiLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly9qb2JieS1uZXh0Ly4vcGFnZXMvYXBpL2F1dGgvWy4uLm5leHRhdXRoXS50cz8yZThiIl0sInNvdXJjZXNDb250ZW50IjpbImltcG9ydCBOZXh0QXV0aCwgeyBBdXRoT3B0aW9ucyB9IGZyb20gXCJuZXh0LWF1dGhcIjtcclxuaW1wb3J0IENyZWRlbnRpYWxzUHJvdmlkZXIgZnJvbSBcIm5leHQtYXV0aC9wcm92aWRlcnMvY3JlZGVudGlhbHNcIjtcclxuZXhwb3J0IGNvbnN0IGF1dGhPcHRpb25zIDogQXV0aE9wdGlvbnMgPSB7XHJcbiAgc2VjcmV0OiBwcm9jZXNzLmVudi5ORVhUQVVUSF9TRUNSRVQsXHJcbiAgcHJvdmlkZXJzOiBbXHJcbiAgICBDcmVkZW50aWFsc1Byb3ZpZGVyKHtcclxuICAgICAgbmFtZTogXCJDcmVkZW50aWFsc1wiLFxyXG4gICAgICBjcmVkZW50aWFsczoge1xyXG4gICAgICAgIHVzZXJuYW1lOiB7IGxhYmVsOiBcIlVzZXJuYW1lXCIsIHR5cGU6IFwidGV4dFwiLCBwbGFjZWhvbGRlcjogXCJqc21pdGhcIiB9LFxyXG4gICAgICAgIHBhc3N3b3JkOiB7IGxhYmVsOiBcIlBhc3N3b3JkXCIsIHR5cGU6IFwicGFzc3dvcmRcIiB9LFxyXG4gICAgICB9LFxyXG4gICAgICBhc3luYyBhdXRob3JpemUoY3JlZGVudGlhbHMsIHJlcSkge1xyXG4gICAgICAgIGNvbnN0IHsgdXNlcm5hbWUsIHBhc3N3b3JkIH0gPSBjcmVkZW50aWFscztcclxuXHJcbiAgICAgICAgY29uc3QgaHR0cHMgPSByZXF1aXJlKFwiaHR0cHNcIik7XHJcbiAgICAgICAgY29uc3QgYWdlbnQgPSBuZXcgaHR0cHMuQWdlbnQoe1xyXG4gICAgICAgICAgcmVqZWN0VW5hdXRob3JpemVkOiBmYWxzZSxcclxuICAgICAgICB9KTtcclxuXHJcbiAgICAgICAgY29uc3Qgb3B0aW9ucyA9IHtcclxuICAgICAgICAgIG1ldGhvZDogXCJQT1NUXCIsXHJcbiAgICAgICAgICBoZWFkZXJzOiB7XHJcbiAgICAgICAgICAgIFwiQ29udGVudC1UeXBlXCI6IFwiYXBwbGljYXRpb24vanNvblwiLFxyXG4gICAgICAgICAgfSxcclxuICAgICAgICAgIGFnZW50LFxyXG4gICAgICAgICAgYm9keTogSlNPTi5zdHJpbmdpZnkoe1xyXG4gICAgICAgICAgICB1c2VybmFtZSxcclxuICAgICAgICAgICAgcGFzc3dvcmQsXHJcbiAgICAgICAgICB9KSxcclxuICAgICAgICB9O1xyXG5cclxuICAgICAgICBjb25zdCByZXMgPSBhd2FpdCBmZXRjaChcclxuICAgICAgICAgIGBodHRwczovL2xvY2FsaG9zdDo2MDAxL2FwaS9hdXRoL2xvZ2luYCxcclxuICAgICAgICAgIG9wdGlvbnNcclxuICAgICAgICApO1xyXG5cclxuICAgICAgICBjb25zdCB1c2VyID0gYXdhaXQgcmVzLmpzb24oKTtcclxuXHJcbiAgICAgICAgaWYgKHJlcy5vayAmJiB1c2VyKSB7XHJcbiAgICAgICAgICByZXR1cm4gdXNlcjtcclxuICAgICAgICB9IGVsc2UgcmV0dXJuIG51bGw7XHJcbiAgICAgIH0sXHJcbiAgICB9KSxcclxuICBdLFxyXG4gIHNlc3Npb246IHtcclxuICAgIHN0cmF0ZWd5OiBcImp3dFwiLFxyXG4gIH0sXHJcbiAgcGFnZXM6IHtcclxuICAgIHNpZ25JbjogXCIvbG9naW5cIixcclxuICB9LFxyXG4gIGNhbGxiYWNrczoge1xyXG4gICAgYXN5bmMgand0KHsgdG9rZW4sIHVzZXIgfSkge1xyXG4gICAgICBpZiAodXNlcikge1xyXG4gICAgICAgIHRva2VuLmFjY2Vzc1Rva2VuID0gdXNlci5hY2Nlc3NUb2tlbjtcclxuICAgICAgfVxyXG4gICAgICByZXR1cm4gdG9rZW47XHJcbiAgICB9LFxyXG4gICAgYXN5bmMgc2Vzc2lvbih7IHNlc3Npb24sIHRva2VuIH0pIHtcclxuICAgICAgY29uc29sZS5sb2coc2Vzc2lvbik7XHJcblxyXG4gICAgICBzZXNzaW9uLmFjY2Vzc1Rva2VuID0gdG9rZW4uYWNjZXNzVG9rZW47XHJcblxyXG4gICAgICByZXR1cm4gc2Vzc2lvbjtcclxuICAgIH0sXHJcbiAgfSxcclxufTtcclxuZXhwb3J0IGRlZmF1bHQgTmV4dEF1dGgoYXV0aE9wdGlvbnMpO1xyXG4iXSwibmFtZXMiOlsiTmV4dEF1dGgiLCJDcmVkZW50aWFsc1Byb3ZpZGVyIiwiYXV0aE9wdGlvbnMiLCJzZWNyZXQiLCJwcm9jZXNzIiwiZW52IiwiTkVYVEFVVEhfU0VDUkVUIiwicHJvdmlkZXJzIiwibmFtZSIsImNyZWRlbnRpYWxzIiwidXNlcm5hbWUiLCJsYWJlbCIsInR5cGUiLCJwbGFjZWhvbGRlciIsInBhc3N3b3JkIiwiYXV0aG9yaXplIiwicmVxIiwiaHR0cHMiLCJyZXF1aXJlIiwiYWdlbnQiLCJBZ2VudCIsInJlamVjdFVuYXV0aG9yaXplZCIsIm9wdGlvbnMiLCJtZXRob2QiLCJoZWFkZXJzIiwiYm9keSIsIkpTT04iLCJzdHJpbmdpZnkiLCJyZXMiLCJmZXRjaCIsInVzZXIiLCJqc29uIiwib2siLCJzZXNzaW9uIiwic3RyYXRlZ3kiLCJwYWdlcyIsInNpZ25JbiIsImNhbGxiYWNrcyIsImp3dCIsInRva2VuIiwiYWNjZXNzVG9rZW4iLCJjb25zb2xlIiwibG9nIl0sInNvdXJjZVJvb3QiOiIifQ==\n//# sourceURL=webpack-internal:///(api)/./pages/api/auth/[...nextauth].ts\n");

/***/ })

};
;

// load runtime
var __webpack_require__ = require("../../../webpack-api-runtime.js");
__webpack_require__.C(exports);
var __webpack_exec__ = (moduleId) => (__webpack_require__(__webpack_require__.s = moduleId))
var __webpack_exports__ = (__webpack_exec__("(api)/./pages/api/auth/[...nextauth].ts"));
module.exports = __webpack_exports__;

})();