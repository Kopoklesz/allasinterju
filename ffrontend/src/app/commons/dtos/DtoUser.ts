import { DtoJobShort } from "./DtoJobShort";

export interface DtoUser {
    id: number;
    firstName: string;
    lastName: string;
    emailAddress: string;
    BirthDate?: string;
    BirthPlace?: string;
    AppliedJobs?:  Array<DtoJobShort>;
  }