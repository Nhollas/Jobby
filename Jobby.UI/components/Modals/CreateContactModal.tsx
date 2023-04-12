"use client";

import { Dispatch, SetStateAction, useContext, useReducer } from "react";
import { ModalContext } from "contexts/ModalContext";
import reducer from "reducers/CreateContactReducer";
import { ActionButton } from "../Common";
import Input from "components/Common/Input";
import MultiInput from "components/Common/MultiInput";
import { client } from "clients";
import { Contact } from "types";
import { CreateContactRequest } from "types/requests/CreateContactRequest";

interface Props {
  boardId: string;
  setContacts: Dispatch<SetStateAction<Contact[]>>;
}

export const CreateContactModal = ({ boardId, setContacts }: Props) => {
  const { handleModal, closeModal } = useContext(ModalContext);

  const [state, dispatch] = useReducer(reducer, {
    body: {
      boardId: boardId,
      jobIds: ["2108a37c-e2d8-ccbe-c5bb-3a0a7e401001"],
      firstName: "",
      lastName: "",
      jobTitle: "",
      location: "",
      socials: {
        twitterUrl: "",
        facebookUrl: "",
        linkedInUrl: "",
        githubUrl: "",
      },
      emails: [],
      phones: [],
      companies: [],
    },
  });

  const { body } = state;

  const handleChange = (e: any) => {
    dispatch({
      type: "HANDLE_INPUT_CHANGE",
      name: e.target.name,
      value: e.target.value,
    });
  };

  const handleSocialsChange = (e: any) => {
    dispatch({
      type: "HANDLE_SOCIALS_INPUT_CHANGE",
      name: e.target.name,
      value: e.target.value,
    });
  };

  const handleSubmit = async (e: any) => {
    e.preventDefault();

    const formattedBody = {
      ...body,
      companies: body.companies.map((company) => {
        return company.value;
      }),
      emails: body.emails.map((email) => {
        return email.value;
      }),
      phones: [],
    };

    const createdContact = await client.post<any, Contact>(
      "/contact/create",
      formattedBody
    );

    setContacts((contacts) => {
      return [...contacts, createdContact];
    });

    closeModal();
  };

  return (
    <div className="absolute inset-0 top-16 z-10 flex w-full justify-center bg-white/70">
      <div className="mt-8 max-h-[75vh] overflow-y-scroll w-full max-w-md rounded-md border border-slate-200 bg-white p-8">
        <form
          className="flex flex-col gap-y-8"
          method="post"
          onSubmit={handleSubmit}
        >
          <h1 className="text-xl font-medium">Create Contact</h1>
          <div className="flex flex-row w-full">
            <section className="flex flex-col gap-y-5">
              <div className="flex flex-row gap-x-4">
                <Input
                  name="firstName"
                  label="First Name"
                  onChange={handleChange}
                  value={body.firstName}
                  type="text"
                />
                <Input
                  name="lastName"
                  label="Last Name"
                  onChange={handleChange}
                  value={body.lastName}
                  type="text"
                />
              </div>
              <Input
                name="jobTitle"
                label="Job Title"
                onChange={handleChange}
                value={body.jobTitle}
                type="text"
              />
              <Input
                name="location"
                label="Location"
                onChange={handleChange}
                value={body.location}
                type="text"
              />
              <MultiInput
                list={body.companies}
                name="companies"
                label="Companies"
                placeholder="Company"
                onChange={(value) =>
                  dispatch({
                    type: "HANDLE_COMPANY_CHANGE",
                    value,
                  })
                }
                addItem={(value) =>
                  dispatch({
                    type: "HANDLE_COMPANY_ADD",
                    value,
                  })
                }
                removeItem={(index) =>
                  dispatch({
                    type: "HANDLE_COMPANY_REMOVE",
                    value: index,
                  })
                }
              />
              <MultiInput
                list={body.emails}
                name="emails"
                label="Emails"
                placeholder="Email"
                onChange={(value) =>
                  dispatch({
                    type: "HANDLE_EMAIL_CHANGE",
                    value,
                  })
                }
                addItem={(value) =>
                  dispatch({
                    type: "HANDLE_EMAIL_ADD",
                    value,
                  })
                }
                removeItem={(index) =>
                  dispatch({
                    type: "HANDLE_EMAIL_REMOVE",
                    value: index,
                  })
                }
                chooseType
              />
              <MultiInput
                list={body.phones}
                name="phones"
                label="Phones"
                placeholder="Phone"
                onChange={(value) =>
                  dispatch({
                    type: "HANDLE_PHONE_CHANGE",
                    value,
                  })
                }
                addItem={(value) =>
                  dispatch({
                    type: "HANDLE_PHONE_ADD",
                    value,
                  })
                }
                removeItem={(index) =>
                  dispatch({
                    type: "HANDLE_PHONE_REMOVE",
                    value: index,
                  })
                }
                chooseType
              />
              <div className="flex flex-col gap-y-2">
                <p className="text-sm font-medium">Socials</p>
                <Input
                  name="twitterUrl"
                  placeholder="Twitter Url"
                  onChange={handleSocialsChange}
                  value={body.socials.twitterUrl}
                  type="text"
                />
                <Input
                  name="facebookUrl"
                  placeholder="Facebook Url"
                  onChange={handleSocialsChange}
                  value={body.socials.facebookUrl}
                  type="text"
                />
                <Input
                  name="linkedInUrl"
                  placeholder="LinkedIn Url"
                  onChange={handleSocialsChange}
                  value={body.socials.linkedInUrl}
                  type="text"
                />
                <Input
                  name="githubUrl"
                  placeholder="Github Url"
                  onChange={handleSocialsChange}
                  value={body.socials.githubUrl}
                  type="text"
                />
              </div>
            </section>
            <section></section>
          </div>
          <div className="flex flex-row justify-center gap-4">
            <ActionButton
              variant="secondary"
              text="Cancel"
              onClick={() => closeModal()}
            />
            <ActionButton
              variant="primary"
              text="Create"
              type="submit"
              extended
            />
          </div>
        </form>
      </div>
    </div>
  );
};
