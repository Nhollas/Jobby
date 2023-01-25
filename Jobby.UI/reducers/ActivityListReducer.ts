import { Reducer } from 'react';
import { Activity } from '../types';

interface Action {
  type: string;
  name: string;
  activityId: string;
  value: any;
}

interface State {
  activities: Activity[]
}

const reducer: Reducer<State, Action> = (state, action) => {
  const { name, activityId, value } = action;
  switch (action.type) {
    case 'HANDLE_INPUT_CHANGE':
      return {
        ...state,
        activities: state.activities.map(activity => {
          if (activity.id === activityId) {
            return {
              ...activity,
              [name]: value,
            };
          }
          return activity;
        }),
      };
    default:
      return state;
  }
};

export default reducer;
