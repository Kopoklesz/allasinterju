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
  