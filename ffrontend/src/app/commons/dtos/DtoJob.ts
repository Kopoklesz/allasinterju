export interface DtoJob {
    id: number;
    jobTitle: string;
    jobType: string;
    workOrder?: string;
    description : string;
    shortDescription?: string;
    city: string;
    exactLocation: string;
    companyName: string;
  
  }

  export interface DtoJobAdd{
   jobTitle: string;
   jobType: string;
  workOrder:string;
  description: string;
  shortDescription: string;
  location: string;
  deadline: Date;
  }
  