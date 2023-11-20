import React from 'react';
import s from '../styles/Button.module.css';


type ButtonProps = {
    text: string;
    onClick: () => void;
}
function Button({text, onClick }: ButtonProps) {
    return (
        <button className={s.btn} onClick={onClick}>{text}</button>
  );
}

export { Button };