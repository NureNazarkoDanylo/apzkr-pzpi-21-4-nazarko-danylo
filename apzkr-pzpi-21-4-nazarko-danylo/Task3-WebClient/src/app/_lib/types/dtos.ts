export type TokensModel = {
  accessToken: string;
  refreshToken: string;
}

export type PaginatedList<T> = {
  items: T[];
  pageNumber: number;
  totalPages: number;
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

export type WashingMachine = {
  id: string;
  name: string;
  manufacturer: string;
  serialNumber: string;
  description: string | null;
  deviceGroupId: string | null;
}

export enum WashingMachineState {
  Idle,
  Prewash,
  Washing,
  Rinsing,
  Spinning
}

export type WashingMachineOperationStatus = {
  state: WashingMachineState;
  waterTemperatureCelcius: number;
  motorSpeedRPM: number;
  isLidClosed: boolean;
  loadWeightKg: number;
}
