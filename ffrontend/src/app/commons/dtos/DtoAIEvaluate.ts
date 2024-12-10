export interface DtoAIEvaluateInput {
    kerdoivId: number;
    jeloltSzam: number;
    tovabbiPromptBemenet: string;
}

export interface DtoAIEvaluateOutput {
    KitoltottKerdoivId: number;
    MunkakeresoId: number;
    Vezeteknev: string;
    Keresztnev: string;
    Szazalek?: number;
    MIszazalek?: number;
    Tovabbjut?: boolean;
    MIajanlas?: boolean;
}