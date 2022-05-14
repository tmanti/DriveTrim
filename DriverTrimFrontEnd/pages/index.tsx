import type { NextPage } from 'next'
import Head from 'next/head'
import Image from 'next/image'
import styles from '../styles/Home.module.css'
import Logo from '../component/Logo'
import DatePicker from '../component/DatePicker'

export default function homePage() {
  return(
      <div> 
        <div>
          <Logo/>
        </div>
        <div>
          <DatePicker name="Start Date"/>
          <DatePicker name="End Date"/>
        </div>      
      </div>
  )
}

export {}
