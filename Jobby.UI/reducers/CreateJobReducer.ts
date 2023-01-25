import { Reducer } from 'react';
import { Job } from '../types';

interface Action {
  type: string;
  name: string;
  value: any;
}

interface State {
  job: Pick<Job, "company" | "title" | "jobListId" | "boardId">;
}

const reducer: Reducer<State, Action> = (state, action) => {
  const { name, value } = action;
  switch (action.type) {
    case 'HANDLE_INPUT_CHANGE':
      return {
        ...state,
        job: {
          ...state.job,
          [name]: value,
        }
      };
    default:
      return state;
  }
};

export default reducer;
