import { Router, useRouter } from 'next/router';

function Logo() {
    const router = useRouter();
  // Import result is the URL of your image
    return <img src={'/favicon.ico'} alt="Logo" onClick={ () => router.push('/')}/>;
}

export default Logo;