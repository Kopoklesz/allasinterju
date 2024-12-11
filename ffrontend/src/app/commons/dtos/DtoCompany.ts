import { DtoPlace } from "./DtoPlace";

export interface DtoCompany {
    id: number;
    companyName: string;
    companyType: string;
    description?: string;
    mainAdress : string;
    mailingAdress?: string;
    outsideCoummunicationEmployeeM: string;
    mobilePhoneNumber?: number;
    cablePhoneNumber?: number;
    pictureBase64: string;
  }
  
 export interface DtoCompanyRegister {
    email : string;
    password : string;
    companyName: string;
    companyType: string;
    description?: string;
    place?: DtoPlace;
    mailingAddress?: string;
    hrEmployee?: string;
    mobilePhoneNumber?: string;
    cablePhoneNumber?: string;
    pictureBase64?: string;
 } 



 export interface DtoCompanyModify {
   companyName: string;
   companyType: string;
   description: string;
   mainAddress: string;
   mailingAddress: string;
   outsideCommunicationsEmployee: string;
   mobilePhoneNumber: string;
   cablePhoneNumber: string;
 }