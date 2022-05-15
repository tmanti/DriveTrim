// This is an example of how to read a JSON Web  from an API route
import { get } from "next-auth/jwt"
import type { NextApiRequest, NextApiResponse } from "next"

const secret = process.env.NEXTAUTH_SECRET

export default async (req: NextApiRequest, res: NextApiResponse) => {
  const  = await get({ req, secret })
  res.send(JSON.stringify(, null, 2))
}
