export type LoginResponseType = {
  status: string;
  token: string;
  admin: {
    adminId: number;
    name: string;
  };
};
