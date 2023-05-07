import { Reducer } from "react";
import { CreateContactRequest } from "types/requests/CreateContactRequest";

interface Action {
  type: string;
  name?: string;
  value: any;
}

interface State {
  body: CreateContactRequest;
}

const reducer: Reducer<State, Action> = (state, action) => {
  const { name, value } = action;

  switch (action.type) {
    case "HANDLE_INPUT_CHANGE":
      if (!name) return state;

      return {
        ...state,
        body: {
          ...state.body,
          [name]: value,
        },
      };
    case "HANDLE_COMPANY_CHANGE":
      return {
        ...state,
        body: {
          ...state.body,
          companies: state.body.companies.map((company) => {
            if (company.id === value.id) {
              return {
                ...company,
                value: value.value,
              };
            }
            return company;
          }),
        },
      };
    case "HANDLE_COMPANY_ADD":
      return {
        ...state,
        body: {
          ...state.body,
          companies: [...state.body.companies, value],
        },
      };
    case "HANDLE_COMPANY_REMOVE":
      return {
        ...state,
        body: {
          ...state.body,
          companies: state.body.companies.filter(
            (company) => company !== value
          ),
        },
      };
    case "HANDLE_EMAIL_CHANGE":
      return {
        ...state,
        body: {
          ...state.body,
          emails: state.body.emails.map((email) => {
            if (email.id === value.id) {
              return {
                ...email,
                value: value.value,
                name: value.value,
                type: value.type,
              };
            }
            return email;
          }),
        },
      };
    case "HANDLE_EMAIL_ADD":
      return {
        ...state,
        body: {
          ...state.body,
          emails: [...state.body.emails, value],
        },
      };
    case "HANDLE_EMAIL_REMOVE":
      return {
        ...state,
        body: {
          ...state.body,
          emails: state.body.emails.filter((email) => email !== value),
        },
      };
    case "HANDLE_PHONE_CHANGE":
      return {
        ...state,
        body: {
          ...state.body,
          phones: state.body.phones.map((phone) => {
            if (phone.id === value.id) {
              return {
                ...phone,
                value: value.value,
                number: value.value,
                type: value.type,
              };
            }
            return phone;
          }),
        },
      };
    case "HANDLE_PHONE_ADD":
      return {
        ...state,
        body: {
          ...state.body,
          phones: [...state.body.phones, value],
        },
      };
    case "HANDLE_PHONE_REMOVE":
      return {
        ...state,
        body: {
          ...state.body,
          phones: state.body.phones.filter((phone) => phone !== value),
        },
      };
    case "HANDLE_SOCIALS_INPUT_CHANGE":
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
    default:
      return state;
  }
};

export default reducer;
