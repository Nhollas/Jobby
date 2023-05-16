import { Reducer } from "react";

interface Action {
  type: string;
  property: string;
  value: any;
}

type ArrayTypes = {
  companies: Company[];
  emails: Email[];
  phones: Phone[];
};

export type CreateContactRequest = {
  boardId: string | null;
  jobIds: string[];
  firstName: string;
  lastName: string;
  jobTitle: string;
  location: string;
  socials: Social;
  emails: Email[];
  phones: Phone[];
  companies: Company[];
};

export interface Base {
  id: string;
  value: string;
  type?: number;
}

type Company = Base & {
  name: string;
};

type Email = Base & {
  name: string;
};

type Phone = Base & {
  number: string;
};

type Social = {
  twitterUrl: string;
  facebookUrl: string;
  linkedInUrl: string;
  githubUrl: string;
};

interface State {
  body: CreateContactRequest;
}

const handleInputChange = (
  state: State,
  property: string,
  value: any
): State => {
  if (!property) return state;

  return {
    ...state,
    body: {
      ...state.body,
      [property]: value,
    },
  };
};

const handleArrayItemChange = (
  state: State,
  arrayName: keyof ArrayTypes,
  arrayItem: Company | Email | Phone
): State => {
  return {
    ...state,
    body: {
      ...state.body,
      [arrayName]: state.body[arrayName].map((item) => {
        if (item.id === arrayItem.id) {
          return {
            ...item,
            value: arrayItem.value,
            [arrayName === "emails" ? "name" : "number"]: arrayItem.value,
            type: arrayItem.type,
          };
        }
        return item;
      }),
    },
  };
};

const handleArrayItemAdd = (
  state: State,
  arrayName: keyof ArrayTypes,
  arrayItem: Company | Email | Phone
): State => {
  return {
    ...state,
    body: {
      ...state.body,
      [arrayName]: [...state.body[arrayName], arrayItem],
    },
  };
};

const handleArrayItemRemove = (
  state: State,
  arrayName: keyof ArrayTypes,
  arrayItem: Base
): State => {
  const array: Array<Base> = state.body[arrayName];

  const filteredArray = array.filter((item) => item.id !== arrayItem.id);

  return {
    ...state,
    body: {
      ...state.body,
      [arrayName]: filteredArray,
    },
  };
};

const handleSocialsInputChange = (
  state: State,
  name: string,
  value: any
): State => {
  if (!name) return state;

  return {
    ...state,
    body: {
      ...state.body,
      socials: {
        ...state.body.socials,
        [name]: value,
      },
    },
  };
};

const reducer: Reducer<State, Action> = (state, action) => {
  switch (action.type) {
    case "HANDLE_INPUT_CHANGE":
      return handleInputChange(state, action.property, action.value);
    case "HANDLE_ARRAY_ITEM_CHANGE":
      return handleArrayItemChange(
        state,
        action.property as keyof ArrayTypes,
        action.value
      );
    case "HANDLE_ARRAY_ITEM_ADD":
      return handleArrayItemAdd(
        state,
        action.property as keyof ArrayTypes,
        action.value
      );
    case "HANDLE_ARRAY_ITEM_REMOVE":
      return handleArrayItemRemove(
        state,
        action.property as keyof ArrayTypes,
        action.value
      );
    case "HANDLE_SOCIALS_INPUT_CHANGE":
      return handleSocialsInputChange(state, action.property, action.value);
    default:
      return state;
  }
};

export default reducer;
