import { useRouter } from 'next/router';
import styles from '../styles/Navbar.module.css';

const Navbar = ({linkCol}:{ linkCol: string }) =>{//https://github.com/maticzav/nookies#readme
    const router = useRouter();

    return (
        <div className = { styles.navbarBackground }>
            <div className = { styles.navbarLinkContainer }>
                <h1 className = { styles.navbarLink } style = {{ color: linkCol }}> 
                    <img src = '/favicon.ico' style = {{ transform: 'translate(0,-15px)', height: '60px' }} onClick = { () => router.push('/') } />
                </h1>
            </div>
        </div>
    );
}

export default Navbar;