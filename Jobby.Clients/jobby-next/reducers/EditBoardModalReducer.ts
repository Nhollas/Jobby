import { Reducer } from "react";
import { Board } from "../types";

interface Action {
  type: string;
  name: string;
  value: any;
}

interface State {
  board: Board
}

const reducer: Reducer<State, Action> = (state, action) => {
  const { name, value } = action;
  switch (action.type) {
    case "HANDLE_INPUT_CHANGE":
      return {
        ...state,
        [name]: value,
      };
    default:
      return state;
  }
};

export default reducer;