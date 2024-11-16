import { DtoJobShort } from "./DtoJobShort";

export interface DtoUser {
    id: number;
    firstName: string;
    lastName: string;
    emailAddress: string;
    birthDate?: string;
    birthPlace?: string;
    appliedJobs?:  Array<DtoJobShort>;
  }

  export interface DtoUserRegister {
    firstName: string;
    lastName: string;
    emailAddress: string;
    password:string;
    taxNumber?: number;
    mothersName?: string;
    birthDate?: Date;
    birthPlace?: string;
    invitationCode?: string;
  }