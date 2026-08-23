export type LoginResponseType = {
  status: string;
  token: string;
  admin: {
    id: number;
    name: string;
  };
};
