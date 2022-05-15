import Layout from "../components/layout"
import styles from '../styles/Homepage.module.css'
import Logo from '../components/Logo'
import ColorButton from '../components/Button'
import DatePicker from '../components/DatePicker'
import { signIn, signOut, useSession } from "next-auth/react"
import * as React from 'react';
import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';
import { requesttrim } from "../utility/requesttrim"

export default function IndexPage() {
  const session = useSession()

  const [date1, setValue1] = React.useState<Date | null>(null);
  const [date2, setValue2] = React.useState<Date | null>(null);

  function submitTrim(session:any, date1:any, date2:any){
    if(date1 == null){
      return
    }
    var start = {
      day: date1.getDay,
      month: date1.getMonth,
      year: date1.getFullYear
    }

    var end;
    if(date2 == null){
      end = {

      }
    } else {
      end = {
        day: date2.getDay,
        month: date2.getMonth,
        year: date2.getFullYear
      }
    }

    requesttrim(JSON.stringify(session), start, end, ()=>{
      console.log("work pls");
    });
  }

  return (
    <Layout>
      <div id={styles.background}>
      <div id={styles.logo}>
        <Logo/>
      </div>
      <div id={styles.container}>
        <div>
          <h3>From</h3>
          <DatePicker name="Start Date" val={setValue1}/>
        </div>
        <div>
          <h3>To</h3>
          <DatePicker name="End Date"  val ={setValue2}/>
        </div>
      </div>  
      <div id={styles.button}>
        <div>
          {
            session?
              <Stack direction="row" spacing={2}>
               <Button variant="contained" color="success" onClick={()=>submitTrim(session, date1, date2)}>
                 Trim Down Photos
               </Button>
              </Stack>
          :null
          }
        </div>
      </div>
    </div>
    </Layout>
  )
}
