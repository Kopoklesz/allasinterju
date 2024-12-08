import { DtoCompanyShort } from "./DtoCompanyShort";
import { BCompetence } from "./DtoCompetence";

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
    company : DtoCompanyShort;
    deadline: Date;
  }

  export interface DtoJobAdd{
    jobTitle: string;
    jobType: string;
    workOrder:string;
    description: string;
    shortDescription: string;
    location: string;
    deadline: Date;
    competences : Array<BCompetence>
  }
  
  export interface DtoKerdoivLetrehozas{
    nev ?: string,
    kor : number,
    allasId : number,
    kitoltesPerc ?: number,
    kerdesek : Array<DtoKerdesLetrehozas>,
  }

  export interface DtoKerdesLetrehozas{
    kifejtos ?: boolean,
    program ?: boolean,
    valasztos ?: boolean,
    szoveg ?: string,
    programozosAlapszoveg ?: string,
    tesztesetek ?: Array<DtoTesztesetLetrehozas>,
    valaszok ?: Array<DtoKivalasztosLetrehozas>,
  }

  export interface DtoTesztesetLetrehozas{
    bemenet ?: string,
    kimenet ?: string
  }

  export interface DtoKivalasztosLetrehozas {
    valaszSzoveg : string,
    helyes : boolean,
  }
