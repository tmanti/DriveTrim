import Layout from "../components/layout"
import styles from '../styles/Homepage.module.css'
import Logo from '../components/Logo'
import ColorButton from '../components/Button'
import DatePicker from '../components/DatePicker'

export default function IndexPage() {
  return (
    <Layout>
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
    </div>
    </Layout>
  )
}
