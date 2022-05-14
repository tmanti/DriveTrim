import type { NextPage } from 'next'
import Head from 'next/head'
import Image from 'next/image'
import styles from '../styles/Homepage.module.css'
import Logo from '../component/Logo'
import DatePicker from '../component/DatePicker'

export default function homePage() {
  return(
      <div id={styles.background}> 
        <div id={styles.logo}>
          <Logo/>
        </div>
        <div id={styles.container}>
          <div>
            <DatePicker name="Start Date"/>
          </div>
          <div>
            <DatePicker name="End Date"/>
          </div>
        </div>      
      </div>
  )
}

export {}
