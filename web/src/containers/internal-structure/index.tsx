import React, { ReactNode } from 'react';
import { Header } from './header';

interface InternalStructureProps {
    children?: ReactNode;
}

function InternalStructure({ children }: InternalStructureProps) {
  return (
    <>
      <Header />
      <div>
        {children}
      </div>
    </>
  );
}

export { InternalStructure };
