const reducer = (state, action) => {
  const { name, activityId, value } = action;
  switch (action.type) {
    case "HANDLE_INPUT_CHANGE":
      return {
        ...state,
        activities: state.activities.map((activity) => {
          if (activity.id === activityId) {
            return {
              ...activity,
              [name]: value,
            };
          }
          return activity;
        }),
      };
    case "HANDLE_CHECKBOX_INPUT_CHANGE":
      return {
        ...state,
        activities: state.activities.map((activity) => {
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
