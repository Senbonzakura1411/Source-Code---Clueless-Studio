// Copyright Epic Games, Inc. All Rights Reserved.


#include "PhoenixCrossGameModeBase.h"

#include "TopDownCharacter.h"
void APhoenixCrossGameModeBase::BeginPlay()
{
	Super::BeginPlay();

	if (ATopDownCharacter* CustomPlayerController = Cast<ATopDownCharacter>(GetWorld()->GetFirstPlayerController()))
	{
		if (ATopDownCharacter* TopDownCharacter = Cast<ATopDownCharacter>(DefaultPawnClass.GetDefaultObject()))
		{
			CustomPlayerController->Possess(TopDownCharacter);
		}
	}
}
