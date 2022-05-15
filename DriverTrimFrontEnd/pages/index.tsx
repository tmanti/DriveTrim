import styles from '../styles/Homepage.module.css'
import Logo from '../component/Logo'
import ColorButton from '../component/Button'
import DatePicker from '../component/DatePicker'
import GoogleButton from '../component/GoogleButton'

export default function homePage() {
  return(
    <div id={styles.background}>
      <div id={styles.logo}>
        <Logo/>
      </div>
      <div id={styles.container}>
        <div>
          <h3>From</h3>
          <DatePicker name="Start Date"/>
        </div>
        <div>
          <h3>To</h3>
          <DatePicker name="End Date"/>
        </div>
      </div>  
      <div id={styles.button}>
        <div>
          <ColorButton/>
        </div>
      </div>
      <div id={styles.button}>
        <div>
          <GoogleButton/>
        </div>
      </div>    
    </div>
  )
}

export {}
