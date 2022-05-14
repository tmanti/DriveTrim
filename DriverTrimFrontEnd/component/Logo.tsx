import { Router, useRouter } from 'next/router';

function Logo() {
    const router = useRouter();
  // Import result is the URL of your image
    return (
      <div>
        <img src={'/favicon.ico'} alt="Logo" width={50} height={50} onClick={ () => router.push('/')}/>
      </div>
    )
      
}

export default Logo;