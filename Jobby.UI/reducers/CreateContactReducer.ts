import { Reducer } from 'react';
import { Contact } from '../types';

interface Action {
  type: string;
  name: string;
  value: any;
}

interface State {
  contact: Pick<Contact, "company" | "title" | "jobListId" | "boardId" | "colour">;
}

const reducer: Reducer<State, Action> = (state, action) => {
  const { name, value } = action;
  switch (action.type) {
    case 'HANDLE_INPUT_CHANGE':
      return {
        ...state,
        contact: {
          ...state.contact,
          [name]: value,
        }
      };
    default:
      return state;
  }
};

export default reducer;
