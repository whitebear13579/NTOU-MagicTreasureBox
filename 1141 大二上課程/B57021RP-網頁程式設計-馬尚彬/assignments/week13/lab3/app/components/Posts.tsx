"use client";

import { useState } from "react";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { ThumbsUpIcon } from "lucide-react";

export default function Post(props: { description: string }) {
  const [like,setLike] = useState(0);
  return (
    <Card>
        <CardHeader>
            <CardTitle>貓咪小知識</CardTitle>
        </CardHeader>
        <CardContent>
            <CardDescription>
                {props.description}
            </CardDescription>
        </CardContent>
        <CardFooter>
            <button onClick={()=>setLike(like+1)} className="flex items-center gap-2">
                <ThumbsUpIcon/>
                {like}
            </button>
        </CardFooter>
    </Card>
  );
}
