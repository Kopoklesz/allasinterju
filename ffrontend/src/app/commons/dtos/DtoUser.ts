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
    taxNumber: number;
    mothersName: string;
    birthDate: Date;
    birthPlace: string;
    invitationCode: string;
  }

  export interface DtoLogin{
     userName: string;
     password: string;
  }

  export interface DtoUserLeetStats{
    success: boolean;
    totalSolved: number;
    totalQuestions: number;
    easySolved: number;
    totalEasy: number;
    mediumSolved: number;
    totalMedium: number;
    hardSolved: number;
    totalHard: number;
  }