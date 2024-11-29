export interface DtoTest {
  id: number;
  name: string;
  type: string;
  duration: number;
  isCompleted: boolean;
  description?: string;
  order?: number;
  template?: string;
  testCases?: Array<{
    input: string;
    expectedOutput: string;
  }>;
}